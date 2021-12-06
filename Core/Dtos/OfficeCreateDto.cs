using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Core.Dtos
{
    public class OfficeCreateDto
    {
        public int? HospitalAffiliationId { get; set; }
       // public decimal InitialExaminationFee { get; set; }
       // public decimal FollowUpExaminationFee { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }

        [Range(-90, 90)]
        public double Latitude { get; set; }
        [Range(-180, 180)]
        public double Longitude { get; set; }
        public IFormFile Picture { get; set; }

    }
}