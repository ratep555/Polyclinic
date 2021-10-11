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

       
    }
}