namespace Core.Dtos
{
    public class ExaminationToCreateDto
    {
        public string Anamnesis { get; set; }
        public string Diagnosis { get; set; }
        public string Therapy { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }
    }
}