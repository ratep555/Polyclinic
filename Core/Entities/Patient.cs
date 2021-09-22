using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Patient : BaseEntity
    {
        public int ApplicationUserId { get; set; }     

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }   

        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Occupation { get; set; }
        public string Residence { get; set; }

        public int GenderId { get; set; }
        
        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }

        public int EducationId { get; set; }

        [ForeignKey("EducationId")]
        public Education Education { get; set; }

       

    }
}