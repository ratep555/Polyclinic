using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class AppointmentDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }     

        [DataType(DataType.Date)]
        public DateTime DateAndTimeOfAppointment { get; set; }
        public string Remarks { get; set; }

    }
}