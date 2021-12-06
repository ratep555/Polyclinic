using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OfficesController : BaseApiController
    {
        private readonly IOfficeService _officeService;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly IPhotoService _photoService;

        private string container = "offices";

        public OfficesController(IOfficeService officeService, IMapper mapper,
        IFileStorageService fileStorageService,  IPhotoService photoService)
        {
            _officeService = officeService;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<OfficeToReturnDto>>> GetAllOfficesForDoctor(
            [FromQuery] QueryParameters queryParameters)
        {
            var userId = User.GetUserId();

            var count = await _officeService.GetCountForOffices(userId);
            var list = await _officeService.GetOfficesWithSearchingAndPaging(queryParameters, userId);

            var data = _mapper.Map<IEnumerable<OfficeToReturnDto>>(list);

            return Ok(new Pagination<OfficeToReturnDto>
            (queryParameters.Page, queryParameters.PageCount, count, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfficeToReturnDto>> GetOfficeById(int id)
        {
            var office = await _officeService.GetOfficeByIdAsync(id);

            if (office == null) return NotFound();

            return _mapper.Map<OfficeToReturnDto>(office);
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateOffice([FromBody] OfficeDto officeDto)
        {
            var userId = User.GetUserId();

            var doctor = await _officeService.FindDoctorById(userId);

            var office = _mapper.Map<Office1>(officeDto);

            office.Doctor1Id = doctor.Id;

            await _officeService.CreateOffice(office);
            // vraćaj uvijek nocontent na post osim kod vježbe u postmanu, ovo te zezalo, ispravi to u myportfolio
            // ako možeš naravno:)
            return NoContent();
        }

        [HttpPost("pictureattempt")]
        public async Task<ActionResult> CreateOffice1([FromForm] OfficeCreateDto officeDto)
        {
            var userId = User.GetUserId();

            var doctor = await _officeService.FindDoctorById(userId);

            var office = _mapper.Map<Office1>(officeDto);

            office.Doctor1Id = doctor.Id;

            if (officeDto.Picture != null)
            {
                office.Picture = await _fileStorageService.SaveFile(container, officeDto.Picture);
            }


            await _officeService.CreateOffice(office);
            // vraćaj uvijek nocontent na post osim kod vježbe u postmanu, ovo te zezalo, ispravi to u myportfolio
            // ako možeš naravno:)
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OfficeDto>> UpdateOffice(int id,
             [FromBody] OfficeDto officeDto)
        {
            var userId = User.GetUserId();
            var docor = await _officeService.FindDoctorById(userId);

            var office = _mapper.Map<Office1>(officeDto);

            if (id != office.Id) return BadRequest();
            
            office.Doctor1Id = docor.Id;

            await _officeService.UpdateOffice(office);

            return NoContent();
        }

        [HttpPut("first/{id}")]
        public async Task<ActionResult> UpdateOffice1(int id,
             [FromBody] OfficeCreationDto officeDto)
        {
            var userId = User.GetUserId();
            var docor = await _officeService.FindDoctorById(userId);

            var office = await _officeService.GetOfficeByIdAsync(id);

            if (office == null) return NotFound();
            
            office.Doctor1Id = docor.Id;

            office = _mapper.Map(officeDto, office);

            await _officeService.Save();

            return NoContent();
        }
        

        [HttpPost("appointment")]
        public async Task<ActionResult<OfficeDto>> CreateAppointment([FromBody] OfficeDto officeDto)
        {
            var userId = User.GetUserId();

            var doctor = await _officeService.FindDoctorById(userId);

            var office = _mapper.Map<Office1>(officeDto);

            office.Doctor1Id = doctor.Id;

            await _officeService.CreateOffice(office);

            return _mapper.Map<OfficeDto>(office);
        }

        [AllowAnonymous]
        [HttpGet("hospitals")]
        public async Task<ActionResult<List<HospitalAffiliationDto>>> GetHospitals()
        {
            var hospitals = await _officeService.ShowHospitals();

            return _mapper.Map<List<HospitalAffiliationDto>>(hospitals);

        }
        //  cloudinary slike koje šljakaju

        [HttpPost("addphoto/{id}")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(int id, IFormFile file)
        {        
            var office = await _officeService.GetOfficeByIdAsync(id);

            if (office == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (office.PhotoId == null)
            {
                await _officeService.SavePhoto(photo);

                office.PhotoId = photo.Id;

                await _officeService.Save();

                return CreatedAtAction("GetOfficeById", new { id = office.Id }, _mapper.Map<PhotoDto>(photo));
            }     

            if (office.PhotoId != null)
            {
                var previousphoto = await _officeService.FindPhotoById(office.PhotoId);

                office.PhotoId = null;

                var result1 = await _photoService.DeletePhotoAsync(previousphoto.PublicId);
                if (result1.Error != null) return BadRequest(result.Error.Message);

                await _officeService.DeletePhoto(previousphoto);

                await _officeService.SavePhoto(photo);

                office.PhotoId = photo.Id;

                await _officeService.Save();

                return CreatedAtAction("GetOfficeById", new { id = office.Id }, _mapper.Map<PhotoDto>(photo));
            }         

            return NoContent();
        }


        [HttpPost("pictureattempt7")]
        public async Task<ActionResult> CreateOffice7([FromForm] OfficeCreateDto officeDto)
        {
            var userId = User.GetUserId();

            var doctor = await _officeService.FindDoctorById(userId);

            var office = _mapper.Map<Office1>(officeDto);

            office.Doctor1Id = doctor.Id;
            office.InitialExaminationFee = 0;
            office.FollowUpExaminationFee = 0;

            if (officeDto.Picture != null)
            {
                var result = await _photoService.AddPhotoAsync(officeDto.Picture);

                if (result.Error != null) return BadRequest(result.Error.Message);   

                office.Picture = result.SecureUrl.AbsoluteUri;       
            }
            await _officeService.CreateOffice(office);
           
            return NoContent();
        }

      

    }
}





