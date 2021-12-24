using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
    }
}