using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
  
        public int? SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public int? PolyclinicId { get; set; }
        public Polyclinic Polyclinic { get; set; }


    }
}