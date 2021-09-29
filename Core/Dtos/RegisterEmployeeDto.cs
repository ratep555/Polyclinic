using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class RegisterEmployeeDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        public int CategoryId { get; set; }
        
        public int PolyclinicDepartmentId { get; set; }
    }
}