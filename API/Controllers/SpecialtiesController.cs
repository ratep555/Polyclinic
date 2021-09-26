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
    public class SpecialtiesController : BaseApiController
    {
        private readonly ISpecialtyService _specialtyService;
        private readonly IMapper _mapper;
        public SpecialtiesController(ISpecialtyService specialtyService, IMapper mapper)
        {
            _specialtyService = specialtyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<SpecialtyDto>>> GetAllSpecialties(
            [FromQuery] QueryParameters queryParameters)
        {
            var count = await _specialtyService.GetCountForSpecialties();
            var list = await _specialtyService.GetSpecialtiesWithSearchingAndPaging(queryParameters);

            var data = _mapper.Map<IEnumerable<SpecialtyDto>>(list);

            return Ok(new Pagination<SpecialtyDto>
            (queryParameters.Page, queryParameters.PageCount, count, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialtyDto>> GetSpecialtyById(int id)
        {
            var specialty = await _specialtyService.GetSpecialtyByIdAsync(id);

            if (specialty == null) return NotFound();

            return _mapper.Map<SpecialtyDto>(specialty);
        }

        [HttpPost]
        public async Task<ActionResult<SpecialtyDto>> CreateSpecialty([FromBody] SpecialtyDto specialtyDto)
        {
            var specialty = _mapper.Map<Specialty>(specialtyDto);

            await _specialtyService.CreateSpecialty(specialty);

            return _mapper.Map<SpecialtyDto>(specialty);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SpecialtyDto>> UpdateSpecialty(int id, [FromBody] SpecialtyDto specialtyDto)
        {
            var specialty = _mapper.Map<Specialty>(specialtyDto);

            if (id != specialty.Id) return BadRequest();

            await _specialtyService.UpdateSpecialty(specialty);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSpecialty(int id)
        {
            var specialty = await _specialtyService.GetSpecialtyByIdAsync(id);

            if (specialty == null) return NotFound();

            await _specialtyService.DeleteSpecialty(specialty);

            return NoContent();
        }

        
      
    }
}










