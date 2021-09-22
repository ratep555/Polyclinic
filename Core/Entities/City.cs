using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class City : BaseEntity
    {
        public string CityName { get; set; }
        public int CountryId { get; set; }
        
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
    }
}