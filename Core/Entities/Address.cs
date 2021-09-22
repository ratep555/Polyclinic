using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Address : BaseEntity
    {
        public string Street { get; set; }
        public string StreetNumber { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}