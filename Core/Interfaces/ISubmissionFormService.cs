using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ISubmissionFormService
    {
        Task CreateSubmissionForm(SubmissionForm submissionForm);
        Task UpdateSubmissionForm(int id);
        Task<Appointment> MakeAppointment(int id, AppointmentDto appointmentDto);
        Task<Patient> FindPatientById(int id);
    }
}