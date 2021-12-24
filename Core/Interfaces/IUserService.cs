using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task ConfirmEmailAsync(string email, string token);

    }
}