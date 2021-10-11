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
    public class SubmissionFormsController : BaseApiController
    {
        private readonly ISubmissionFormService _submissionFormService;
        private readonly IMapper _mapper;
        public SubmissionFormsController(ISubmissionFormService submissionFormService, IMapper mapper)
        {
            _submissionFormService = submissionFormService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<SubmissionFormDto>> CreateSubmissionForm([FromBody] SubmissionFormDto submissionFormDto)
        {
            var userId = User.GetUserId();

            var submissionForm = _mapper.Map<SubmissionForm>(submissionFormDto);

            submissionForm.ApplicationUserId = userId;
            submissionForm.PrefferedDateOfExamination = DateTime.Now;

            await _submissionFormService.CreateSubmissionForm(submissionForm);

            return _mapper.Map<SubmissionFormDto>(submissionForm);
        }
        
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<ActionResult<SubmissionFormDto>> UpdateSubmissionForm1(int id,
                [FromBody] SubmissionFormDto submissionFormDto)
        {

            var submissionForm = _mapper.Map<SubmissionForm>(submissionFormDto);

            if (id != submissionForm.Id) return BadRequest();

            await _submissionFormService.UpdateSubmissionForm(submissionForm);
          //  await _submissionFormService. CreatePatient(submissionForm.Id);       
           // await _submissionFormService.MakeAppointment1(submissionForm);      

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPut("sellimo/{id}")]
        public async Task<ActionResult<int>> UpdateSubmissionForm2(int id,
                [FromBody] SubmissionFormDto submissionFormDto)
        {

            var submissionForm = _mapper.Map<SubmissionForm>(submissionFormDto);

            if (id != submissionForm.Id) return BadRequest();

            await _submissionFormService.UpdateSubmissionForm(submissionForm);
          //  await _submissionFormService. CreatePatient(submissionForm.Id);       
           var appointment = await _submissionFormService.MakeAppointment1(submissionForm);   
           if (appointment == null) return NotFound();

           await _submissionFormService.DeleteSubmissionform(submissionForm);   

            return appointment.Id;
        }

        [AllowAnonymous]
        [HttpPut("sell/{id}")]
        public async Task<ActionResult<AppointmentDto>> UpdateAppointment(int id,
                [FromBody] AppointmentDto appointmentDto)
        {

            var appointment = _mapper.Map<Appointment>(appointmentDto);

            if (id != appointment.Id) return BadRequest();

            await _submissionFormService.UpdateAppointment(appointment);

            return NoContent();
        }
        
        [AllowAnonymous]
        [HttpGet("list/{id}")]
        public async Task<ActionResult<List<Doctor>>> ShowList(int id)
        {
            var list = await _submissionFormService.ShowListOfDoctors(id);

            return Ok(list);
        }

        [HttpPost("examination")]
        public async Task<ActionResult<ExaminationToReturnDto>> CreateExamination(
            [FromBody] ExaminationToCreateDto examinationToCreateDto)
        {
            var userId = User.GetUserId();

            var examination = _mapper.Map<Examination>(examinationToCreateDto);

            examination.DoctorId = userId;

            await _submissionFormService.MakeExamination(userId, examinationToCreateDto);

            return _mapper.Map<ExaminationToReturnDto>(examination);
        }

    }
}








