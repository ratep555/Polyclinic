using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Patient : BaseEntity
    {
        public int ApplicationUserId { get; set; }     

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }   

        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Occupation { get; set; }
        public string Residence { get; set; }

        



       

    }
}