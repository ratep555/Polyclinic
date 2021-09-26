using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Paging;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<Pagination<GenderDto>>> GetAllGenders(
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
        public async Task<ActionResult<GenderDto>> CreateGender([FromBody] GenderDto genderDto)
        {
            var gender = _mapper.Map<Gender>(genderDto);

            await _genderService.CreateGender(gender);

            return _mapper.Map<GenderDto>(gender);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenderDto>> UpdateGender(int id, [FromBody] GenderDto genderDto)
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










