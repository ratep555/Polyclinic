namespace Core.Dtos
{
    public class DoctorOfficeSpecializationDto
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public decimal InitialExaminationFee { get; set; }
        public decimal FollowUpExaminationFee { get; set; }

    }
}