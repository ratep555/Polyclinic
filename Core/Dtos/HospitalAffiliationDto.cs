using Core.Entities;

namespace Core.Dtos
{
    public class HospitalAffiliationDto
    {
        public int Id { get; set; }
        public string Doctor { get; set; }
        public string HospitalName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}