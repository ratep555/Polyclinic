using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IPatient1Service
    {
        Task<IEnumerable<DoctorOfficeSpecializationDto>> GetDoctorsWithSearchingAndPaging(QueryParameters queryParameters);
        Task<int> GetCountForDoctors();
        Task<List<Office1>> GetOfficesWithSearchingPagingSorting(QueryParameters queryParameters);       
        Task<int> GetCountForOffices();
        Task<List<Specialization1>> GetSpecializationsAsync();
        Task<List<Patient1>> GetAllDoctorPatients(int userId, QueryParameters queryParameters);
        Task<int> GetCountForAllDoctorPatients(int userId);
        Task<List<Gender>> GetGenders();




       
    }
}