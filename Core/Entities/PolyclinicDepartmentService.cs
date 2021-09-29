using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class PolyclinicDepartmentService : BaseEntity
    {
        public int PolyclinicId { get; set; }
        [ForeignKey("PolyclinicId")]
        public Polyclinic Polyclinic { get; set; }

        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public int ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
    }
}