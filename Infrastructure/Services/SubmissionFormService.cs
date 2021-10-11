using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class SubmissionFormService : ISubmissionFormService
    {
        private readonly PolyclinicContext _context;
        public SubmissionFormService(PolyclinicContext context)
        {
            _context = context;
        }

        public async Task CreateSubmissionForm(SubmissionForm submissionForm)
        {
            _context.SubmissionForms.Add(submissionForm);
            await _context.SaveChangesAsync();         

            await CreatePhoneNumber(submissionForm);           
        }

        private async Task CreatePhoneNumber(SubmissionForm submissionForm)
        {
            var user = await _context.Users.Where(x => x.Id == submissionForm.ApplicationUserId).FirstOrDefaultAsync();

            user.PhoneNumber = submissionForm.Phone;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubmissionForm(SubmissionForm submissionForm)
        {
            _context.Entry(submissionForm).State = EntityState.Modified;        
             await _context.SaveChangesAsync();       

              await CreatePatient(submissionForm.Id);

             await MakeAppointment(submissionForm); 
        }

         public async Task CreatePatient(int id)
        {
            var submissionForm = await _context.SubmissionForms.Where(x => x.Id == id).FirstOrDefaultAsync();
            
            var user = await _context.Users
                       .Where(x => x.Id == submissionForm.ApplicationUserId).FirstOrDefaultAsync();

            var patientExists = await _context.Patients
                                .Where(x => x.ApplicationUserId == submissionForm.ApplicationUserId)
                                .AnyAsync();

            Patient patient;

            if (!patientExists)
            {
                patient = new Patient();
                patient.Name = string.Format("{0} {1}", user.FirstName, user.LastName);

                patient.ApplicationUserId = submissionForm.ApplicationUserId;

                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();                    
            }
            else
            {
                patient = await _context.Patients.Where(x => x.ApplicationUserId == submissionForm.ApplicationUserId)
                                .FirstOrDefaultAsync();

                patient.DateOfBirth = DateTime.Now;
                _context.Entry(patient).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                    
            }

        }

        public async Task MakeAppointment(SubmissionForm submissionForm)
        {
            var patient = await _context.Patients.Where(x => x.ApplicationUserId == submissionForm.ApplicationUserId)
                          .FirstOrDefaultAsync();

            var appointment = new Appointment();
            appointment.PatientId = patient.Id;
            appointment.DepartmentId = submissionForm.DepartmentId;
            appointment.StartDateAndTimeOfAppointment = submissionForm.PrefferedDateOfExamination;
            appointment.EndDateAndTimeOfAppointment = DateTime.Now;

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

        }
        public async Task<Appointment> MakeAppointment1(SubmissionForm submissionForm)
        {
            var patient = await _context.Patients.Where(x => x.ApplicationUserId == submissionForm.ApplicationUserId)
                          .FirstOrDefaultAsync();

            var appointment = await _context.Appointments.Where(x => x.PatientId == patient.Id 
                              && x.DepartmentId == submissionForm.DepartmentId)
                              .FirstOrDefaultAsync();          

            return appointment;

        }

        public async Task DeleteSubmissionform(SubmissionForm submissionForm)
        {
            _context.SubmissionForms.Remove(submissionForm);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointment(Appointment appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Doctor>> ShowListOfDoctors(int id)
        {
            return await _context.Doctors.Where(x => x.DepartmentId == id).ToListAsync();
        }

        public async Task MakeExamination(int userId, ExaminationToCreateDto examinationToCreateDto)
        {
            var doctor = await _context.Doctors.Include(x => x.ApplicationUser) 
                         .Where(x => x.ApplicationUserId == userId)
                         .FirstOrDefaultAsync();

            var examination = new Examination();
            examination.PatientId = examinationToCreateDto.PatientId;
            examination.Doctor = doctor;
            examination.Doctor.Name = doctor.Name;
            examination.DoctorId = doctor.Id;
            examination.Diagnosis = examinationToCreateDto.Diagnosis;
            examination.Anamnesis = examinationToCreateDto.Anamnesis;
            examination.Therapy = examinationToCreateDto.Therapy;

            _context.Examinations.Add(examination);
            await _context.SaveChangesAsync();

        }

        public async Task<Patient> FindPatientById(int id)
        {
            return await _context.Patients.Include(x => x.ApplicationUser)
                   .Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        private  async Task<Doctor> ReturnDoctor()
        {
            return await _context.Doctors.Include(x => x.ApplicationUser)
                   .FirstOrDefaultAsync();
        }
        private  async Task<Doctor> ReturnDoctor1(int userId)
        {
            return await _context.Doctors.Include(x => x.ApplicationUser)
                         .Where(x => x.ApplicationUserId == userId)
                         .FirstOrDefaultAsync();
        }


    }
}






