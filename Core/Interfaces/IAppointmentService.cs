using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<Office1>> GetDoctorOffices(int userId);
        Task CreateAppointment(Appointment1 appointment);
        Task CreateAppointment1(Appointment1Dto appointmentDto);
        Task UpdateAppointment(Appointment1 appointment);
        Task<Patient1> FindPatientById(int userId);
        Task<List<Appointment1>> GetAppointmentsForDoctorWithSearchingAndPaging(QueryParameters queryParameters, int userId);
        Task<int> GetCountForAppointmentsForDoctor(int userId);
        Task<List<Appointment1>> GetAppointmentsForAllPatientsWithSearchingAndPaging(QueryParameters queryParameters);
        Task<int> GetCountForAppointmentsForAllPatients();
        Task<Appointment1> FindAppointmentById(int id);
        Task<List<Appointment1>> GetAvailableAppointmentsForOfficeForPatientsWithSearchingAndPaging(
                int id, QueryParameters queryParameters);
        Task<int> GetCountForAvailableAppointmentsForOfficesForAllPatients(int id);
        Task<Office1> FindOfficeById(int id);



    }
}