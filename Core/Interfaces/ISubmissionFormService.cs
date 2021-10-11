using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ISubmissionFormService
    {
        Task CreateSubmissionForm(SubmissionForm submissionForm);
        Task UpdateSubmissionForm(SubmissionForm submissionForm);
        Task UpdateAppointment(Appointment appointment);
        Task DeleteSubmissionform(SubmissionForm submissionForm);


        Task CreatePatient(int id);

       // Task MakeAppointment1(SubmissionForm submissionForm);
        Task<Appointment> MakeAppointment1(SubmissionForm submissionForm);


        Task<Patient> FindPatientById(int id);
        Task MakeExamination(int userId, ExaminationToCreateDto examinationToCreateDto);
        Task<IEnumerable<Doctor>> ShowListOfDoctors(int id);


    }
}