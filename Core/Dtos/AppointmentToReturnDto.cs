using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class AppointmentToReturnDto
    {
        public string Patient { get; set; }
        public string Doctor { get; set; }   

        [DataType(DataType.Date)]
        public DateTime DateAndTimeOfAppointment { get; set; }
        public string Remark { get; set; }
    }
}