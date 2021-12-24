using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var apiKey = _config["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("petar.sardelic@outlook.com", "Doctor Auth Demo");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);        }
    }
}










