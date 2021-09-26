using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Polyclinic : BaseEntity
    {
        public string PolyclinicName { get; set; }
        public int Established { get; set; }       
        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }
    }
}