using System;

namespace Core.Dtos
{
    public class Patient1Dto
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }     
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string MBO { get; set; }
    }
}