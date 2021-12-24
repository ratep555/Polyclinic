using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenderService
    {
        Task<List<Gender>> GetGendersWithSearchingAndPaging(QueryParameters queryParameters);
        Task<int> GetCountForGenders();
        Task<Gender> GetGenderByIdAsync(int id);
        Task CreateGender(Gender gender);
        Task UpdateGender(Gender gender);
        Task DeleteGender(Gender gender);
    }
}