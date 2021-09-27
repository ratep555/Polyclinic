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
        public async Task UpdateSubmissionForm(int id)
        {
            var submissionForm = await _context.SubmissionForms.Where(x => x.Id == id).FirstOrDefaultAsync();

            submissionForm.Status = true;
            _context.Entry(submissionForm).State = EntityState.Modified;        
            await _context.SaveChangesAsync();       

            await CreatePatient(submissionForm.Id);             
        }

        private async Task CreatePatient(int id)
        {
            var submissionForm = await _context.SubmissionForms.Where(x => x.Id == id).FirstOrDefaultAsync();
            
            var user = await _context.Users
                       .Where(x => x.Id == submissionForm.ApplicationUserId).FirstOrDefaultAsync();

            var patientExists = await _context.Patients
                                .Where(x => x.ApplicationUserId == submissionForm.ApplicationUserId)
                                .AnyAsync();

            if (!patientExists)
            {
                var patient = new Patient();
                patient.Name = string.Format("{0} {1}", user.FirstName, user.LastName);

                patient.ApplicationUserId = submissionForm.ApplicationUserId;
                patient.GenderId = null;
                patient.EducationId = null;

                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();                    
            }
        }

        public async Task<Appointment> MakeAppointment(int id, AppointmentDto appointmentDto)
        {
            var appointment = new Appointment();
            appointment.Doctor = await ReturnDoctor();
            appointment.DoctorId = appointmentDto.DoctorId;
            appointment.DateAndTimeOfAppointment = DateTime.Now;
            appointment.Remark = appointmentDto.Remarks;
            appointment.PatientId = id;

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return appointment;
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






