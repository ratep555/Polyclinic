using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class Appointment1Dto
    {
        public int Id { get; set; }
        public int? Patient1Id { get; set; }     
        public int Office1Id { get; set; }     

        [DataType(DataType.Date)]
        public DateTime StartDateAndTimeOfAppointment { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDateAndTimeOfAppointment { get; set; }

        public bool? Status { get; set; }
        public string Remarks { get; set; }

    }
}