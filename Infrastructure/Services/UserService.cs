using System.Text;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private PolyclinicContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IConfiguration _config;
        private IEmailService _emailService; 
        public UserService(
            PolyclinicContext context,
            UserManager<ApplicationUser> userManager, 
            IConfiguration config, 
            IEmailService emailService) 
        {
            _context = context;
            _userManager = userManager;
            _config = config;
            _emailService = emailService; 
        }

        public async Task ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);
        }



    }
}