using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Appointment : BaseEntity
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        public int? DoctorId { get; set; } 
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }   

        [DataType(DataType.Date)]
        public DateTime? DateAndTimeOfAppointment { get; set; }
        public string Remark { get; set; }

    }
}



