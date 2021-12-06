using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class MedicalRecord1
    {
        [Key, ForeignKey("Person")]
        public int Appointment1Id { get; set; }
        public string AnamnesisDiagnosisTherapy { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        public Appointment1 Appointment { get; set; }
    }
}