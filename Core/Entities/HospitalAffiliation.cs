using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class HospitalAffiliation : BaseEntity
    {
        public int Doctor1Id { get; set; }
        [ForeignKey("Doctor1Id")]
        public Doctor1 Doctor { get; set; }

        public string HospitalName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}