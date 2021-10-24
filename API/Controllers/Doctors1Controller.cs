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
    public class Doctors1Controller : BaseApiController
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;
        private readonly IRatingService _ratingService;
        public Doctors1Controller(IDoctorService doctorService, IMapper mapper, IRatingService ratingService)
        {
            _ratingService = ratingService;
            _doctorService = doctorService;
            _mapper = mapper;
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
    }
}











