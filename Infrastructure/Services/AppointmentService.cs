using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Paging;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly PolyclinicContext _context;
        public AppointmentService(PolyclinicContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment1>> GetAppointmentsForDoctorWithSearchingAndPaging(QueryParameters queryParameters, 
            int userId)
        {
            IQueryable<Appointment1> appointment = _context.Appointments1.Include(x => x.Patient)
                                                   .Include(x => x.Office).ThenInclude(x => x.Doctor)
                                                   .Where(x => x.Office.Doctor.ApplicationUserId == userId)
                                                   .AsQueryable().OrderBy(x => x.StartDateAndTimeOfAppointment);
            
            if (queryParameters.HasQuery())
            {
                appointment = appointment
                .Where(x => x.Office.Street.Contains(queryParameters.Query)
                || x.Patient.Name.Contains(queryParameters.Query));
            }

            if (!string.IsNullOrEmpty(queryParameters.Sort))
            {
                switch (queryParameters.Sort)
                {
                    case "all":
                        appointment = appointment.OrderBy(p => p.StartDateAndTimeOfAppointment);
                        break;
                    case "pending":
                        appointment = appointment.Where(p => p.Status == null & p.Patient1Id == null);
                        break;
                    case "booked":
                        appointment = appointment.Where(p => p.Status == null && p.Patient1Id != null);
                        break;
                    case "confirmed":
                        appointment = appointment.Where(p => p.Status == true && p.Patient1Id != null);
                        break;
                    case "cancelled":
                        appointment = appointment.Where(p => p.Status == false && p.Patient1Id != null);
                        break;
                    default:
                        appointment = appointment
                            .Where(n => n.StartDateAndTimeOfAppointment > DateTime.Now && n.Patient1Id != null);
                        break;
                }
            }    

            appointment = appointment.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);
            
            return await appointment.ToListAsync();        
        }

        public async Task<Doctor1> FindDoctorById1(int userId)
        {
            return await _context.Doctors1.Where(x => x.ApplicationUserId == userId)
                         .FirstOrDefaultAsync();
        }
        public async Task<int> GetCountForAppointmentsForDoctor(int userId)
        {
            return await _context.Appointments1.Include(x => x.Office).ThenInclude(x => x.Doctor)
                        .Where(x => x.Office.Doctor.ApplicationUserId == userId).CountAsync();
        }

        public async Task<List<Appointment1>> GetAppointmentsForAllPatientsWithSearchingAndPaging(
                QueryParameters queryParameters)
        {
            IQueryable<Appointment1> appointment = _context.Appointments1.Include(x => x.Patient)
                                                   .Include(x => x.Office).ThenInclude(x => x.Doctor)
                                                   .AsQueryable().OrderByDescending(x => x.StartDateAndTimeOfAppointment);
            
            if (queryParameters.HasQuery())
            {
                appointment = appointment
                .Where(x => x.Office.City.Contains(queryParameters.Query));
            }

            appointment = appointment.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);
            
            if (!string.IsNullOrEmpty(queryParameters.Sort))
            {
                switch (queryParameters.Sort)
                {
                    case "dateAsc":
                        appointment = appointment.OrderBy(p => p.StartDateAndTimeOfAppointment);
                        break;
                    default:
                        appointment = appointment.OrderByDescending(n => n.StartDateAndTimeOfAppointment);
                        break;
                }
            }    
            
            return await appointment.ToListAsync();        
        }

        public async Task<int> GetCountForAppointmentsForAllPatients()
        {
            return await _context.Appointments1.CountAsync();
        }

        public async Task<List<Appointment1>> GetAppointmentsForSpecificPatient(int userId,
                QueryParameters queryParameters)
        {
            IQueryable<Appointment1> appointment = _context.Appointments1.Include(x => x.Patient)
                                                   .Include(x => x.Office).ThenInclude(x => x.Doctor)
                                                   .Where(x => x.Patient.ApplicationUserId == userId)
                                                   .AsQueryable().OrderBy(x => x.StartDateAndTimeOfAppointment);
            
            if (queryParameters.HasQuery())
            {
                appointment = appointment
                .Where(x => x.Office.Street.Contains(queryParameters.Query));
            }

            appointment = appointment.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);
            
            if (!string.IsNullOrEmpty(queryParameters.Sort))
            {
                switch (queryParameters.Sort)
                {
                     case "booked":
                        appointment = appointment.Where(p => p.Status == null && p.Patient1Id != null);
                        break;
                    case "confirmed":
                        appointment = appointment.Where(p => p.Status == true && p.Patient1Id != null);
                        break;
                    case "previous":
                        appointment = appointment.
                            Where(p => p.EndDateAndTimeOfAppointment < DateTime.Now && p.Patient1Id != null);
                        break;
                    case "dateDesc":
                        appointment = appointment.OrderByDescending(p => p.StartDateAndTimeOfAppointment);
                        break;
                    default:
                        appointment = appointment.OrderBy(n => n.StartDateAndTimeOfAppointment);
                        break;
                }
            }    
            
            return await appointment.ToListAsync();        
        }

        public async Task<int> GetCountForAppointmentsForSpecificPatient(int userId)
        {
            return await _context.Appointments1.Include(x => x.Patient)
                        .Where(x => x.Patient.ApplicationUserId == userId).CountAsync();
        }


        public async Task<List<Appointment1>> GetAvailableAppointmentsForOfficeForPatientsWithSearchingAndPaging(
                int id, QueryParameters queryParameters)
        {
            IQueryable<Appointment1> appointment = _context.Appointments1
                                                   .Where(x => x.Office1Id == id && x.Patient1Id == null)
                                                   .Include(x => x.Patient)
                                                   .Include(x => x.Office).ThenInclude(x => x.Doctor)
                                                   .AsQueryable().OrderBy(x => x.StartDateAndTimeOfAppointment);
            
            if (queryParameters.HasQuery())
            {
                appointment = appointment
                .Where(x => x.Office.Street.Contains(queryParameters.Query));
            }

            appointment = appointment.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);
            
            if (!string.IsNullOrEmpty(queryParameters.Sort))
            {
                switch (queryParameters.Sort)
                {
                    case "dateDesc":
                        appointment = appointment.OrderByDescending(p => p.StartDateAndTimeOfAppointment);
                        break;
                    default:
                        appointment = appointment.OrderBy(n => n.StartDateAndTimeOfAppointment);
                        break;
                }
            }    
            
            return await appointment.ToListAsync();        
        }

        public async Task<int> GetCountForAvailableAppointmentsForOfficesForAllPatients(int id)
        {
            return await _context.Appointments1.Where(x => x.Office1Id == id && x.Patient1Id == null)
                         .CountAsync();
        }


        public async Task CreateAppointment(Appointment1 appointment)
        {
            _context.Appointments1.Add(appointment);
            await _context.SaveChangesAsync();
        }
        
        public async Task CreateAppointment1(Appointment1Dto appointmentDto)
        {
            var value1 = appointmentDto.StartDateAndTimeOfAppointment;
            var value2 = appointmentDto.EndDateAndTimeOfAppointment;
            var appointment = new Appointment1();
            appointment.Office1Id = appointmentDto.Office1Id;
            appointment.StartDateAndTimeOfAppointment = value1.AddHours(2);
            appointment.EndDateAndTimeOfAppointment = value2.AddHours(2);
            _context.Appointments1.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointment(Appointment1 appointment)
        {
            
             _context.Entry(appointment).State = EntityState.Modified;        
             await _context.SaveChangesAsync();
        }

        public async Task<Patient1> FindPatientById(int userId)
        {
            return await _context.Patients1.Where(x => x.ApplicationUserId == userId)
                         .FirstOrDefaultAsync();
        }

        public async Task<Appointment1> FindAppointmentById(int id)
        {
            return await _context.Appointments1.Include(x => x.Office).ThenInclude(x => x.Doctor)
                         .Include(x => x.Patient)
                         .Where(x => x.Id == id)
                         .FirstOrDefaultAsync();
        }

        public async Task BookAppointmentByUser(int id, int userId)
        {
            var appointment = await _context.Appointments1.Include(x => x.Office).ThenInclude(x => x.Doctor)
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
            
            
        }


        public async Task<List<Office1>> GetDoctorOffices(int userId)
        {
            return await _context.Offices.Where(x => x.Doctor.ApplicationUserId == userId)
                         .ToListAsync();
        }

        public async Task<Office1> FindOfficeById(int id)
        {
            return await _context.Offices.Where(x => x.Id == id)
                         .FirstOrDefaultAsync();
        }

        public async Task<List<Appointment1>> GetAllAvailableAppointmentsForAllVisitors(QueryParameters queryParameters)
        {
                                 
            var doctorSpecialization = await _context.DoctorSpecializations.
                                       Where(x => x.Specialization1Id == queryParameters.SpecializationId) 
                                       .FirstOrDefaultAsync();

            IQueryable<Appointment1> appointments = _context.Appointments1.Include(x => x.Office)
                    .ThenInclude(x => x.Doctor)
                    .Where(x => x.Patient1Id == null && x.StartDateAndTimeOfAppointment < DateTime.Now)
                    .AsQueryable().OrderByDescending(x => x.StartDateAndTimeOfAppointment);
            
            if (queryParameters.HasQuery())
            {
                appointments = appointments
                .Where(x => x.Office.City.Contains(queryParameters.Query));
            }

            if (queryParameters.SpecializationId.HasValue)
            {
                appointments = appointments.Where(x => x.Office.Doctor1Id == doctorSpecialization.Doctor1Id);

            }

            appointments = appointments.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);

            if (!string.IsNullOrEmpty(queryParameters.Sort))
            {
                switch (queryParameters.Sort)
                {
                    case "dateAsc":
                        appointments = appointments.OrderBy(p => p.StartDateAndTimeOfAppointment);
                        break;
                    default:
                        appointments = appointments.OrderByDescending(n => n.StartDateAndTimeOfAppointment);
                        break;
                }
            }           
            return await appointments.ToListAsync();        
        }

        public async Task<int> GetCountForAllAvailableAppointmentsForAllVisitors()
        {
            return await _context.Appointments1
                .Where(x => x.Patient1Id == null && x.StartDateAndTimeOfAppointment > DateTime.Now)
                .CountAsync();
        }




    }
}








