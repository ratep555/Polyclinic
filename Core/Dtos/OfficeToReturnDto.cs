using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class OfficeToReturnDto
    {
        public int Id { get; set; }
        public string Doctor { get; set; }     
        public int DoctorId { get; set; }   
        public decimal InitialExaminationFee { get; set; }
        public decimal FollowUpExaminationFee { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}