using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class Appointment1ToReturnDto
    {
        public int Id { get; set; }
        public string Patient { get; set; } 
        public string Doctor { get; set; } 
        public string OfficeAddress { get; set; }     
        public string City { get; set; }     

        [DataType(DataType.Date)]
        public DateTime StartDateAndTimeOfAppointment { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDateAndTimeOfAppointment { get; set; }

        public bool? Status { get; set; }
        public string Remarks { get; set; }
    }
}