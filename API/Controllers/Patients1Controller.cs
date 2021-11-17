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
    public class Patients1Controller : BaseApiController
    {
        private readonly IPatient1Service _patient1Service;
        private readonly IMapper _mapper;
        public Patients1Controller(IPatient1Service patient1Service, IMapper mapper)
        {
            _patient1Service = patient1Service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<DoctorOfficeSpecializationDto>>> GetAllDoctors(
            [FromQuery] QueryParameters queryParameters)
        {
            var count = await _patient1Service.GetCountForDoctors();
            var list = await _patient1Service.GetDoctorsWithSearchingAndPaging(queryParameters);

            return Ok(new Pagination<DoctorOfficeSpecializationDto>
            (queryParameters.Page, queryParameters.PageCount, count, list));
        }

        [HttpGet("offices")]
         public async Task<ActionResult<Pagination<OfficeToReturnDto>>> GetAllOffices(
            [FromQuery] QueryParameters queryParameters)
        {

            var count = await _patient1Service.GetCountForOffices();
            var list = await _patient1Service.GetOfficesWithSearchingPagingSorting(queryParameters);

            var data = _mapper.Map<IEnumerable<OfficeToReturnDto>>(list);

            return Ok(new Pagination<OfficeToReturnDto>
            (queryParameters.Page, queryParameters.PageCount, count, data));
        }

        [Authorize]
        [HttpGet("yahoo")]
        public async Task<ActionResult<Pagination<Patient1Dto>>> GetDoctorPatients(
                [FromQuery] QueryParameters queryParameters)
        {
            var userId = User.GetUserId();

            var count = await _patient1Service.GetCountForAllDoctorPatients(userId);
            var list = await _patient1Service.GetAllDoctorPatients(userId, queryParameters);

            var data = _mapper.Map<IEnumerable<Patient1Dto>>(list);

            return Ok(new Pagination<Patient1Dto>
            (queryParameters.Page, queryParameters.PageCount, count, data));        
        }

        [HttpGet("specializations")]
        public async Task<ActionResult<List<Specialization1>>> GetSpecializations()
        {
            var list = await _patient1Service.GetSpecializationsAsync();

            return Ok(list);
        }
    }
}









