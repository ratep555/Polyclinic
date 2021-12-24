using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class Doctors1Controller : BaseApiController
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;
        private readonly IRatingService _ratingService;
        private readonly IFileStorageService _fileStorageService;
        private string container = "doctors1";

        public Doctors1Controller(IDoctorService doctorService, IMapper mapper, 
        IRatingService ratingService, IFileStorageService fileStorageService)
        {
            _ratingService = ratingService;
            _doctorService = doctorService;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }

        [HttpGet("alldoctors")]
         public async Task<ActionResult<Pagination<Doctor1Dto>>> GetAllDoctors(
            [FromQuery] QueryParameters queryParameters)
        {
            var count = await _doctorService.GetCountForAllDoctors();
            var list = await _doctorService
                .GetAllDoctors(queryParameters);

            var data = _mapper.Map<IEnumerable<Doctor1Dto>>(list);

            return Ok(new Pagination<Doctor1Dto>
            (queryParameters.Page, queryParameters.PageCount, count, data));
        }

        [HttpGet("doctorlist")]
         public async Task<ActionResult<Pagination<Doctor1Dto>>> GetAllDoctorsList()
        {
            var list = await _doctorService
                .ShowListOfAllDoctors();


            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor1Dto>> GetDoctor(int id)
        {
            var doctor = await _doctorService.FindDoctorById(id);

            if (doctor == null) return NotFound();

            var averageVote = 0.0;
            var userVote = 0;

            if (await _doctorService.ChechIfAny(id))
            {
                averageVote = await _doctorService.AverageVote(id);
            }

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = User.GetUserId();

                var ratingDb = await _ratingService.FindCurrentRate(id, userId);

                if (ratingDb != null)
                {
                    userVote = ratingDb.Rate;
                }
            }

            var doctorToReturn = _mapper.Map<Doctor1Dto>(doctor);

            doctorToReturn.AverageVote = averageVote;
            doctorToReturn.UserVote = userVote;

            return Ok(doctorToReturn);
        }

        [HttpGet("appuserid/{id}")]
        public async Task<ActionResult<Doctor1Dto>> GetDoctorByApplicationUserId(int id)
        {
            var doctor = await _doctorService.FindDoctorByApplicationUserId(id);

            if (doctor == null) return NotFound();

            var averageVote = 0.0;
            var userVote = 0;

            if (await _doctorService.ChechIfAny(doctor.Id))
            {
                averageVote = await _doctorService.AverageVote(doctor.Id);
            }

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = User.GetUserId();

                var ratingDb = await _ratingService.FindCurrentRate(doctor.Id, userId);

                if (ratingDb != null)
                {
                    userVote = ratingDb.Rate;
                }
            }

            var doctorToReturn = _mapper.Map<Doctor1Dto>(doctor);

            doctorToReturn.AverageVote = averageVote;
            doctorToReturn.UserVote = userVote;

            // ovdje ćeš probati zajednički dto
            var doctorWithQualificationsAndOffices = new DoctorWithQualificationsAndOfficesDto();

            doctorWithQualificationsAndOffices.Doctor = doctorToReturn;

            var specialization = await _doctorService.GetAllSpecializationsForDoctor(doctor.Id);
            var specializationToReturn = _mapper.Map<List<SpecializationDto>>(specialization);
            doctorWithQualificationsAndOffices.Specializations = specializationToReturn;

            var subspecialization = await _doctorService.GetAllSubSpecializationsForDoctor(doctor.Id);
            var subspecializationToReturn = _mapper.Map<List<SubspecializationDto>>(subspecialization);
            doctorWithQualificationsAndOffices.Subspecializations = subspecializationToReturn;

            var professionalAssociations = await _doctorService.GetAllProfessionalAssociationsForDoctor(doctor.Id);
            var associationToReturn = _mapper.Map<List<ProfessionalAssociationDto>>(professionalAssociations);
            doctorWithQualificationsAndOffices.ProfessionalAssociations = associationToReturn;

            var publications = await _doctorService.GetAllPublicationsForDoctor(doctor.Id);
            var publicationToReturn = _mapper.Map<List<PublicationDto>>(publications);
            doctorWithQualificationsAndOffices.Publications = publicationToReturn;

            var offices = await _doctorService.GetOfficesForDoctor(doctor.Id);
            var officeToReturn = _mapper.Map<List<OfficeToReturnDto>>(offices);
            doctorWithQualificationsAndOffices.Offices = officeToReturn;

            return Ok(doctorToReturn);
        }

        [HttpGet("appuserid1/{id}")]
        public async Task<ActionResult<DoctorPutGetDto>> GetDoctorByApplicationUserIdForEditing(int id)
        {
            var doctor = await _doctorService.FindDoctorById(id);

            if (doctor == null) return NotFound();

            var doctorToReturn =  _mapper.Map<Doctor1Dto>(doctor);

            var specializationsSelectedIds = doctorToReturn.Specializations.Select(x => x.Id).ToList();

            var nonSelectedSpecialisations = await _doctorService.GetNonSelectedSpecializations(specializationsSelectedIds);
            
            var nonSelectedSpecializationsDto = _mapper.Map<List<SpecializationDto>>(nonSelectedSpecialisations);

            var response = new DoctorPutGetDto();
            response.Doctor = doctorToReturn;
            response.SelectedSpecializations = doctorToReturn.Specializations;
            response.NonSelectedSpecializations = nonSelectedSpecializationsDto;

            return response;
        }

        [HttpPut("editingdoctor/{id}")]
        public async Task<ActionResult> UserUpdatingHisProfile(int id, EditDoctor1Dto editDoctorDto)
        {
            var doctor = await _doctorService.FindDoctorById(id);

            if (doctor == null) return NotFound();

            doctor =  _mapper.Map(editDoctorDto, doctor);

            await _doctorService.Save();

            return NoContent();
        }

        // ne zaboravi dodati u startup app.UseStaticFiles();
        [HttpPut("editingdoctor1/{id}")]
        public async Task<ActionResult> UserUpdatingHisProfile1(int id, [FromForm] EditDoctor1Dto editDoctorDto)
        {
            var doctor = await _doctorService.FindDoctorById(id);

            if (doctor == null) return NotFound();

            doctor =  _mapper.Map(editDoctorDto, doctor);

            if (editDoctorDto.Picture != null)
            {
                doctor.Picture = await _fileStorageService.SaveFile(container, editDoctorDto.Picture);
            }

           /*  if (editDoctorDto.Picture != null)
            {
                doctor.Picture = await _fileStorageService.EditFile(container, editDoctorDto.Picture,
                    doctor.Picture);
            }
 */
            await _doctorService.Save();

            return NoContent();
        }



        [HttpGet("specializations/{id}")]
        public async Task<ActionResult<List<SpecializationDto>>> GetSpecializationsForDoctor(int id)
        {
            var doctor = await _doctorService.FindDoctorById(id);

            if (doctor == null) return NotFound();

            var specialization = await _doctorService.GetAllSpecializationsForDoctor(id);

            var specializationToReturn = _mapper.Map<List<SpecializationDto>>(specialization);

            return Ok(specializationToReturn);
        }

        [HttpGet("subspecializations/{id}")]
        public async Task<ActionResult<List<SubspecializationDto>>> GetSubspecializationsForDoctor(int id)
        {
            var doctor = await _doctorService.FindDoctorById(id);

            if (doctor == null) return NotFound();

            var subspecialization = await _doctorService.GetAllSubSpecializationsForDoctor(id);

            var subspecializationToReturn = _mapper.Map<List<SubspecializationDto>>(subspecialization);

            return Ok(subspecializationToReturn);
        }

        [HttpGet("associations/{id}")]
        public async Task<ActionResult<List<ProfessionalAssociationDto>>> GetProfessionalAssociationsForDoctor(int id)
        {
            var doctor = await _doctorService.FindDoctorById(id);

            if (doctor == null) return NotFound();

            var professionalAssociations = await _doctorService.GetAllProfessionalAssociationsForDoctor(id);

            var associationToReturn = _mapper.Map<List<ProfessionalAssociationDto>>(professionalAssociations);

            return Ok(associationToReturn);
        }

        [HttpGet("publications/{id}")]
        public async Task<ActionResult<List<PublicationDto>>> GetPublicationsForDoctor(int id)
        {
            var doctor = await _doctorService.FindDoctorById(id);

            if (doctor == null) return NotFound();

            var publications = await _doctorService.GetAllPublicationsForDoctor(id);

            var publicationToReturn = _mapper.Map<List<PublicationDto>>(publications);

            return Ok(publicationToReturn);
        }

        [HttpGet("offices/{id}")]
        public async Task<ActionResult<List<OfficeToReturnDto>>> GetOfficesForDoctor(int id)
        {
            var doctor = await _doctorService.FindDoctorById(id);

            if (doctor == null) return NotFound();

            var offices = await _doctorService.GetOfficesForDoctor(id);

            var officeToReturn = _mapper.Map<List<OfficeToReturnDto>>(offices);

            return Ok(officeToReturn);
        }

        [AllowAnonymous]
        [HttpGet("specializations1/{id}")]
        public async Task<ActionResult<List<SpecializationDto>>> GetSpecializationsForDoctorByUserid(int id)
        {

            var specialization = await _doctorService.GetAllSpecializationsForDoctorByUserId(id);

            var specializationToReturn = _mapper.Map<List<SpecializationDto>>(specialization);

            return Ok(specializationToReturn);
        }

        [HttpGet("subspecializations1/{id}")]
        public async Task<ActionResult<List<SubspecializationDto>>> GetSubspecializationsForDoctorByUserid(int id)
        {
            var subspecialization = await _doctorService.GetAllSubSpecializationsForDoctorByUserId(id);

            var subspecializationToReturn = _mapper.Map<List<SubspecializationDto>>(subspecialization);

            return Ok(subspecializationToReturn);
        }

        [HttpGet("associations1/{id}")]
        public async Task<ActionResult<List<ProfessionalAssociationDto>>> GetProfessionalAssociationsForDoctorByUserid(int id)
        {
            var professionalAssociations = await _doctorService.GetAllProfessionalAssociationsForDoctorByUserId(id);

            var associationToReturn = _mapper.Map<List<ProfessionalAssociationDto>>(professionalAssociations);

            return Ok(associationToReturn);
        }

        [HttpGet("publications1/{id}")]
        public async Task<ActionResult<List<PublicationDto>>> GetPublicationsForDoctorByUserid(int id)
        {
            var publications = await _doctorService.GetAllPublicationsForDoctorByUserId(id);

            var publicationToReturn = _mapper.Map<List<PublicationDto>>(publications);

            return Ok(publicationToReturn);
        }

        [HttpGet("offices1/{id}")]
        public async Task<ActionResult<List<OfficeToReturnDto>>> GetOfficesForDoctorByUserid(int id)
        {
            var offices = await _doctorService.GetOfficesForDoctorByUserId(id);

            var officeToReturn = _mapper.Map<List<OfficeToReturnDto>>(offices);

            return Ok(officeToReturn);
        }



        [AllowAnonymous]
        [HttpGet("filter")]
         public async Task<ActionResult<List<Doctor1Dto>>> GetFelipesDoctors(
            [FromQuery] QueryParameters queryParameters)
        {
            var list = await _doctorService
                .GetFelipesDoctors(queryParameters);

            return _mapper.Map<List<Doctor1Dto>>(list);
            
        }

        [AllowAnonymous]
        [HttpGet("filter2")]
         public async Task<ActionResult<List<Doctor1Dto>>> GetFelipesDoctors2(
            [FromQuery] DoctorDto2 dto)
        {
            var list = await _doctorService
                .GetFelipesDoctors1(dto);

            return _mapper.Map<List<Doctor1Dto>>(list);
            
        }

        [HttpGet("filter3")]
         public async Task<ActionResult<Pagination<Doctor1Dto>>> Filter3(
            [FromQuery] DoctorDto3 dto)
        {
            var count = await _doctorService.GetCountForAllDoctors();
            var list = await _doctorService
                .GetFelipesDoctors2(dto);

            var data = _mapper.Map<IEnumerable<Doctor1Dto>>(list);

            return Ok(new Pagination<Doctor1Dto>
            (dto.Page, dto.PageCount, count, data));
        }
        
        [AllowAnonymous]
        [HttpGet("filter1")]
         public async Task<ActionResult<List<Doctor1Dto>>> GetFelipesDoctors1(
            [FromQuery] QueryParameters queryParameters)
        {
            var list = await _doctorService
                .GetFelipesDoctors(queryParameters);

            return _mapper.Map<List<Doctor1Dto>>(list);
            
        }

        [AllowAnonymous]
        [HttpGet("multiplemodel")]
         public async Task<ActionResult<List<SpecializationDto>>> GetSpecializations()
        {
            var list = await _doctorService
                .GetAllSpecializations();

            return _mapper.Map<List<SpecializationDto>>(list);
            
        }
    }
}











