using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Paging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GendersController : BaseApiController
    {
        private readonly IGenderService _genderService;
        private readonly IMapper _mapper;

        public GendersController(IGenderService genderService, IMapper mapper)
        {
            _genderService = genderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<GenderDto>>> GetAllGendersForAdminList(
            [FromQuery] QueryParameters queryParameters)
        {
            var count = await _genderService.GetCountForGenders();
            
            var list = await _genderService.GetGendersWithSearchingAndPaging(queryParameters);

            var data = _mapper.Map<IEnumerable<GenderDto>>(list);

            return Ok(new Pagination<GenderDto>
            (queryParameters.Page, queryParameters.PageCount, count, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenderDto>> GetGenderById(int id)
        {
            var gender = await _genderService.GetGenderByIdAsync(id);

            if (gender == null) return NotFound();

            return _mapper.Map<GenderDto>(gender);
        }

        [HttpPost]
        public async Task<ActionResult<GenderDto>> CreateGender([FromBody] GenderDto genderDTO)
        {
            var gender = _mapper.Map<Gender>(genderDTO);

            await _genderService.CreateGender(gender);

            return CreatedAtAction("GetGenderById", new {id = gender.Id }, 
                _mapper.Map<GenderDto>(gender));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGender(int id, [FromBody] GenderDto genderDto)
        {
            var gender = _mapper.Map<Gender>(genderDto);

            if (id != gender.Id) return BadRequest();

            await _genderService.UpdateGender(gender);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGender(int id)
        {
            var gender = await _genderService.GetGenderByIdAsync(id);

            if (gender == null) return NotFound();

            await _genderService.DeleteGender(gender);

            return NoContent();
        }      
    }
}


