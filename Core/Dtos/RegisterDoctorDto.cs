using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class RegisterDoctorDto
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

        public int SpecialtyId { get; set; }
        
        public int DepartmentId { get; set; }

    }
}