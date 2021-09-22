using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class MedicalChart : BaseEntity
    {
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }


        public int? DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
        

        public string Summary { get; set; }
        public string HistoryOfIllness { get; set; }
        public string Diagnosys { get; set; }
        public string Therapy { get; set; }
    }
}