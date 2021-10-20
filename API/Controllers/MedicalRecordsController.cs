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
    public class MedicalRecordsController : BaseApiController
    {
        private readonly IMedicalRecordService _medicalRecordService;
        private readonly IOfficeService _officeService;
        private readonly IMapper _mapper;
        public MedicalRecordsController(IMedicalRecordService medicalRecordService, 
            IOfficeService officeService,   
            IMapper mapper)
        {
            _medicalRecordService = medicalRecordService;
            _officeService = officeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> CreateMedicalRecord([FromBody] MedicalRecordDto medicalRecordDto)
        {

            var medicalrecord = _mapper.Map<MedicalRecord>(medicalRecordDto);

            medicalrecord.Created = DateTime.Now;

            await _medicalRecordService.CreateMedicalRecord(medicalrecord);
           
            return NoContent();
        }

    }
}