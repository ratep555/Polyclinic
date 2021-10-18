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
    public class OfficesController : BaseApiController
    {
        private readonly IOfficeService _officeService;
        private readonly IMapper _mapper;
        public OfficesController(IOfficeService officeService, IMapper mapper)
        {
            _officeService = officeService;
            _mapper = mapper;
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
        public async Task<ActionResult<OfficeDto>> GetOfficeById(int id)
        {
            var office = await _officeService.GetOfficeByIdAsync(id);

            if (office == null) return NotFound();

            return _mapper.Map<OfficeDto>(office);
        }
        
        [HttpPost]
        public async Task<ActionResult<OfficeDto>> CreateOffice([FromBody] OfficeDto officeDto)
        {
            var userId = User.GetUserId();

            var office = _mapper.Map<Office1>(officeDto);

            await _officeService.CreateOffice(userId, officeDto);

            return _mapper.Map<OfficeDto>(office);
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

            var office = _mapper.Map<Office1>(officeDto);

            await _officeService.CreateOffice(userId, officeDto);

            return _mapper.Map<OfficeDto>(office);
        }


    }
}





