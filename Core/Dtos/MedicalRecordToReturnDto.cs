using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class MedicalRecordToReturnDto
    {
        public int Id { get; set; }
        public int Appointment1Id { get; set; }

        public string AnamnesisDiagnosisTherapy { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        public string Doctor { get; set; }
        public string Patient { get; set; }
        public string Office { get; set; }     
    }
}