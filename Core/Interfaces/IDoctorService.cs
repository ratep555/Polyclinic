using System.Threading.Tasks;
using Core.Dtos;

namespace Core.Interfaces
{
    public interface IDoctorService
    {
        Task CreateDoctor(int userId, RegisterDoctorDto registerDoctorDto, string firstname, string lastname);

    }
}