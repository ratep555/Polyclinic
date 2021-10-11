using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class DoctorSpecialization1 : BaseEntity
    {
        public int Doctor1Id { get; set; }
        [ForeignKey("Doctor1Id")]
        public Doctor1 Doctor { get; set; }

        public int Specialization1Id { get; set; }
        [ForeignKey("Specialization1Id")]
        public Specialization1 Specialization { get; set; }
    }
}