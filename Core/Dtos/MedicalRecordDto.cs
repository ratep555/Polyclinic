using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class MedicalRecordDto
    {
        public int Id { get; set; }
        public string AnamnesisDiagnosisTherapy { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        public int Patient1Id { get; set; }
        public int Office1Id { get; set; }     
    }
}