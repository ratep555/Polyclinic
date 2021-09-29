using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Employee : BaseEntity
    {
        public int ApplicationUserId { get; set; }     

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }   

        public string Name { get; set; }                    
        public string Residence { get; set; }
        public string Summary { get; set; }
        public bool? CurrentlyEmployed { get; set; }

        public int PolyclinicDepartmentId { get; set; }
        [ForeignKey("PolyclinicDepartmentId")]
        public PolyclinicDepartment PolyclinicDepartment { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}

