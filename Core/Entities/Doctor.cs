using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Doctor : BaseEntity
    {
        public int ApplicationUserId { get; set; }     

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }           
        
        public DateTime DateOfBirth { get; set; }       
        public string Residence { get; set; }

        public int SpecialtyId { get; set; }

        [ForeignKey("SpecialtyId")]
        public Specialty Specialty { get; set; }

    }
}











