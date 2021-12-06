using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class MedicalRecordDto1
    {
        public int Appointment1Id { get; set; }
        public string AnamnesisDiagnosisTherapy { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
    }
}