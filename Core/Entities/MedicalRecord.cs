using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class MedicalRecord : BaseEntity
    {
        public string AnamnesisDiagnosisTherapy { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        public int Patient1Id { get; set; }
        [ForeignKey("Patient1Id")]
        public Patient1 Patient { get; set; }

        public int Office1Id { get; set; }     
        [ForeignKey("Office1Id")]
        public Office1 Office { get; set; }     
    }
}