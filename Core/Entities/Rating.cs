using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Rating : BaseEntity
    {
        [Range(1, 5)]
        public int Rate { get; set; }

        public int Patient1Id { get; set; }
        [ForeignKey("Patient1Id")]
        public Patient1 Patient { get; set; }

        public int Doctor1Id { get; set; }
        [ForeignKey("Doctor1Id")]
        public Doctor1 Doctor { get; set; }

    }
}