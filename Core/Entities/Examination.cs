using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Examination : BaseEntity
    {
        public string Anamnesis { get; set; }
        public string Diagnosis { get; set; }
        public string Therapy { get; set; }

        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
    }
}