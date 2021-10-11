using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int DepartmentId { get; set; }     

        [DataType(DataType.Date)]
        public DateTime? StartDateAndTimeOfAppointment { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? EndDateAndTimeOfAppointment { get; set; }
        public string Remarks { get; set; }

    }
}