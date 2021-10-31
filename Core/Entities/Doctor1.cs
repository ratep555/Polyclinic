using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Doctor1 : BaseEntity
    {
        public int ApplicationUserId { get; set; }     

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }   
        
        public string Name { get; set; }
        public string Resume { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartedPracticing { get; set; }
        
        public ICollection<Office1> Offices { get; set; }
        public ICollection<Rating> Ratings { get; set; }       
        public ICollection<DoctorSpecialization1> DoctorSpecializations { get; set; }
        public ICollection<DoctorSpecialization2> DoctorSpecializations2 { get; set; }
        public ICollection<DoctorProfessionalAssociation> DoctorProfessionalAssociations { get; set; }
    }
}