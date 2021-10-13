using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class AppointmentSingleDto
    {
        public int Id { get; set; }
        public int? Patient1Id { get; set; }     
        public int Office1Id { get; set; }     
        public string Doctor { get; set; }     
        public string Office { get; set; }     
        public string Patient { get; set; }     
        public string City { get; set; }     
        public string Country { get; set; }     
        
        [DataType(DataType.Date)]
        public DateTime StartDateAndTimeOfAppointment { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDateAndTimeOfAppointment { get; set; }

        public string Status { get; set; }
        public string Remarks { get; set; }
    }
}