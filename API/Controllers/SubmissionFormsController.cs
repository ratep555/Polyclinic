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
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSubmissionForm(int id)
        {

            await _submissionFormService.UpdateSubmissionForm(id);

            return Ok();
        }
        
        [AllowAnonymous]
        [HttpPost("appointment/{id}")]
        public async Task<ActionResult<AppointmentToReturnDto>> MakeAppointmentStock(int id, AppointmentDto appointmentDto)
        {     
            var patient = await _submissionFormService.FindPatientById(id);

            if (patient == null) return NotFound();

            var appointment = await _submissionFormService.MakeAppointment(id, appointmentDto);                                                                                                

            var appointmentToReturn = _mapper.Map<AppointmentToReturnDto>(appointment);

            return Ok(appointmentToReturn);
        }

    }
}








