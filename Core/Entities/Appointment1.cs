using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Appointment1 : BaseEntity
    {
        public int? Patient1Id { get; set; }     
        [ForeignKey("Patient1Id")]
        public Patient1 Patient { get; set; } 

        public int Office1Id { get; set; }     
        [ForeignKey("Office1Id")]
        public Office1 Office { get; set; }     


        [DataType(DataType.Date)]
        public DateTime StartDateAndTimeOfAppointment { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDateAndTimeOfAppointment { get; set; }

        public bool? Status { get; set; }
        public string Remarks { get; set; }



        
    }
}