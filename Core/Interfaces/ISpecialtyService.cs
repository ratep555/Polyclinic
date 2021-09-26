using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ISpecialtyService
    {
        Task<List<Specialty>> GetSpecialtiesWithSearchingAndPaging(QueryParameters queryParameters);
        Task<int> GetCountForSpecialties();
        Task<Specialty> GetSpecialtyByIdAsync(int id);
        Task CreateSpecialty(Specialty specialty);
        Task UpdateSpecialty(Specialty specialty);
        Task DeleteSpecialty(Specialty specialty);




    }
}