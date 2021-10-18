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
    public class AppointmentsController : BaseApiController
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;
        public AppointmentsController(IAppointmentService appointmentService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        [HttpGet]
         public async Task<ActionResult<Pagination<Appointment1ToReturnDto>>> GetAllAppointmentsForDoctor(
            [FromQuery] QueryParameters queryParameters)
        {
            var userId = User.GetUserId();

            var count = await _appointmentService.GetCountForAppointmentsForDoctor(userId);
            var list = await _appointmentService
                             .GetAppointmentsForDoctorWithSearchingAndPaging(queryParameters, userId);

            var data = _mapper.Map<IEnumerable<Appointment1ToReturnDto>>(list);

            return Ok(new Pagination<Appointment1ToReturnDto>
            (queryParameters.Page, queryParameters.PageCount, count, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentSingleDto>> GetAppointmentById(int id)
        {
            var appointment = await _appointmentService.FindAppointmentById(id);

            if (appointment == null) return NotFound();

            return _mapper.Map<AppointmentSingleDto>(appointment);
        }

        [HttpGet("app/{id}")]
        public async Task<ActionResult<Appointment1Dto>> GetAppointmentForEditById(int id)
        {
            var appointment = await _appointmentService.FindAppointmentById(id);

            if (appointment == null) return NotFound();

            return _mapper.Map<Appointment1Dto>(appointment);
        }

        [HttpGet("allpatients")]
         public async Task<ActionResult<Pagination<Appointment1ToReturnDto>>> GetAllAppointmentsForAllPatients(
            [FromQuery] QueryParameters queryParameters)
        {
            var count = await _appointmentService.GetCountForAppointmentsForAllPatients();
            var list = await _appointmentService
                             .GetAppointmentsForAllPatientsWithSearchingAndPaging(queryParameters);

            var data = _mapper.Map<IEnumerable<Appointment1ToReturnDto>>(list);

            return Ok(new Pagination<Appointment1ToReturnDto>
            (queryParameters.Page, queryParameters.PageCount, count, data));
        }

        [HttpGet("officeappointments/{id}")]
         public async Task<ActionResult<Pagination<Appointment1ToReturnDto>>> GetAllAvailableAppointmentsFroOfficeForPatients(
            int id, [FromQuery] QueryParameters queryParameters)
        {
            var count = await _appointmentService.GetCountForAvailableAppointmentsForOfficesForAllPatients(id);
            var list = await _appointmentService
                             .GetAvailableAppointmentsForOfficeForPatientsWithSearchingAndPaging(
                                 id, queryParameters);

            var data = _mapper.Map<IEnumerable<Appointment1ToReturnDto>>(list);

            return Ok(new Pagination<Appointment1ToReturnDto>
            (queryParameters.Page, queryParameters.PageCount, count, data));
        }

        [HttpGet("first/{id}")]
        public async Task<ActionResult<OfficeDto>> GetOfficeById(int id)
        {
            var office = await _appointmentService.FindOfficeById(id);

            if (office == null) return NotFound();

            return _mapper.Map<OfficeDto>(office);
        }


        [HttpPost]
        public async Task<ActionResult<Appointment1Dto>> CreateAppointmentByDoctor([FromBody] Appointment1Dto appointmentDto)
        {
            var appointment = _mapper.Map<Appointment1>(appointmentDto);
             
            //stavio si createappointment1 samo za probu jer ti ne daje točno vrijeme, ovako šljaka
           // await _appointmentService.CreateAppointment1(appointmentDto);
           
            await _appointmentService.CreateAppointment(appointment);

            return _mapper.Map<Appointment1Dto>(appointment);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<Appointment1Dto>> UpdateAppointmentByPatient(int id,
             [FromBody] Appointment1Dto appointmentDto)
        {
            var userId = User.GetUserId();
            var patient = await _appointmentService.FindPatientById(userId);

            var appointment = await _appointmentService.FindAppointmentById(id);

            if (appointment == null) return NotFound();
            
            appointment.Patient1Id = patient.Id;
            appointment.Remarks = appointmentDto.Remarks;

            await _appointmentService.UpdateAppointment(appointment);

            return NoContent();
        }

        [HttpPut("doc/{id}")]
        public async Task<ActionResult<Appointment1Dto>> UpdateAppointmentByDoctorNew(int id,
             [FromBody] Appointment1Dto appointmentDto)
        {
           var appointment = _mapper.Map<Appointment1>(appointmentDto);

            if (id != appointment.Id) return BadRequest();

            await _appointmentService.UpdateAppointment(appointment);

            return NoContent();
        }
        
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Appointment1Dto>> UpdateAppointmentByDoctor(int id,
             [FromBody] Appointment1Dto appointmentDto)
        {
            var appointment = _mapper.Map<Appointment1>(appointmentDto);

            if (id != appointment.Id) return BadRequest();

            await _appointmentService.UpdateAppointment(appointment);

            return NoContent();
        }

        [HttpPut("updatissimo/{id}")]
        public async Task<ActionResult> BookAppointmentAsPatient(int id)
        {
            var userId = User.GetUserId();
            var patient = await _appointmentService.FindPatientById(userId);

            var appointment = await _appointmentService.FindAppointmentById(id);

            if (appointment == null) return NotFound();

            appointment.Patient1Id = patient.Id;

            await _appointmentService.UpdateAppointment(appointment);

            return NoContent();
        }

        [HttpPut("updatissimocancel/{id}")]
        public async Task<ActionResult> CancelAppointmentAsPatient(int id)
        {
            var userId = User.GetUserId();
            var patient = await _appointmentService.FindPatientById(userId);

            var appointment = await _appointmentService.FindAppointmentById(id);

            if (appointment == null) return NotFound();

            appointment.Patient1Id = null;
            appointment.Remarks = "";

            await _appointmentService.UpdateAppointment(appointment);

            return NoContent();
        }

        [HttpGet("offices")]
        public async Task<ActionResult<IEnumerable<Office1>>> GetDoctorsOffices()
        {
            var userId = User.GetUserId();

            var list = await _appointmentService.GetDoctorOffices(userId);

            return Ok(list);
        }
    }
}






