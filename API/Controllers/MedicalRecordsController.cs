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
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient1Dto>> GetPatient(int id)
        {
            var patient = await _medicalRecordService.FindPatientById(id);

            if (patient == null) return NotFound();

            var patientToReturn = _mapper.Map<Patient1Dto>(patient);
            
            return Ok(patientToReturn);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> CreateMedicalRecord(int id, [FromBody] MedicalRecordDto medicalRecordDto)
        {
            var medicalrecord = _mapper.Map<MedicalRecord>(medicalRecordDto);

            medicalrecord.Patient1Id = id;
            medicalrecord.Created = DateTime.Now;

            await _medicalRecordService.CreateMedicalRecord(medicalrecord);
           
            return NoContent();
        }

        [HttpGet("records/{id}")]
         public async Task<ActionResult<Pagination<MedicalRecordToReturnDto>>> GetMedicalRecordsForPatient(
            int id, [FromQuery] QueryParameters queryParameters)
        {
            var userId = User.GetUserId();

            var patient = await _medicalRecordService.FindPatientById(id);

            if (patient == null) return NotFound();

            var count = await _medicalRecordService.GetCountForMedicalRecordsForPatient(id, userId);

            var list = await _medicalRecordService.GetMedicalRecordsForPatient(id, userId, queryParameters);

            var data = _mapper.Map<List<MedicalRecordToReturnDto>>(list);

            return Ok(new Pagination<MedicalRecordToReturnDto>
            (queryParameters.Page, queryParameters.PageCount, count, data));
        }

        [HttpGet("allrecords")]
         public async Task<ActionResult<Pagination<MedicalRecordToReturnDto>>> GetMedicalRecordsForAllDoctorPatients(
            [FromQuery] QueryParameters queryParameters)
        {
            var userId = User.GetUserId();

            var count = await _medicalRecordService.GetCountForMedicalRecordsForAllDoctorPatients(userId);

            var list = await _medicalRecordService.GetMedicalRecordsForAllDoctorPatients(userId, queryParameters);

            var data = _mapper.Map<List<MedicalRecordToReturnDto>>(list);

            return Ok(new Pagination<MedicalRecordToReturnDto>
            (queryParameters.Page, queryParameters.PageCount, count, data));
        }

        [HttpGet("allrecords1")]
         public async Task<ActionResult<Pagination<MedicalRecordToReturnDto>>> GetMedicalRecordsForAllPatientDoctors(
            [FromQuery] QueryParameters queryParameters)
        {
            var userId = User.GetUserId();

            var count = await _medicalRecordService.GetCountForMedicalRecordsForAllPatientDoctors(userId);

            var list = await _medicalRecordService.GetMedicalRecordsForAllPatientDoctors(userId, queryParameters);

            var data = _mapper.Map<List<MedicalRecordToReturnDto>>(list);

            return Ok(new Pagination<MedicalRecordToReturnDto>
            (queryParameters.Page, queryParameters.PageCount, count, data));
        }

        [HttpGet("singlerecord/{id}")]
        public async Task<ActionResult<MedicalRecordToReturnDto>> GetMedicalRecord(int id)
        {
            var record = await _medicalRecordService.FindMedicalRecordById(id);

            if (record == null) return NotFound();

            var recordToReturn = _mapper.Map<MedicalRecordToReturnDto>(record);
            
            return Ok(recordToReturn);
        }

        [HttpGet("offices")]
         public async Task<ActionResult<List<OfficeToReturnDto>>> GetOfficesForDoctor()
        {
            var userId = User.GetUserId();

            var list = await _medicalRecordService.GetOfficesForDoctor(userId);

            var data = _mapper.Map<List<OfficeToReturnDto>>(list);

            return Ok(data);
        }

        [HttpGet("offices1")]
         public async Task<ActionResult<List<OfficeToReturnDto>>> GetAllOffices()
        {
            var list = await _medicalRecordService.GetAllOffices();

            var data = _mapper.Map<List<OfficeToReturnDto>>(list);

            return Ok(data);
        }



    }
}












