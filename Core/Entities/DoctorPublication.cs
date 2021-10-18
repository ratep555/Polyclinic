using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class DoctorPublication : BaseEntity
    {
        public int Doctor1Id { get; set; }
        [ForeignKey("Doctor1Id")]
        public Doctor1 Doctor { get; set; }

        public int Publication1Id { get; set; }
        [ForeignKey("Publication1Id")]
        public Publication1 Publication { get; set; }
    }
}