using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IOfficeService
    {
        Task CreateOffice(Office1 office);
        Task<List<Office1>> GetOfficesWithSearchingAndPaging(QueryParameters queryParameters, int userId);
        Task<int> GetCountForOffices(int userId);
        Task UpdateOffice(Office1 office);
        Task<Doctor1> FindDoctorById(int userId);
        Task<Office1> GetOfficeByIdAsync(int id);
        Task Save();


        

    }
}