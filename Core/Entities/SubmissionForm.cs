using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class SubmissionForm : BaseEntity
    {
       public int ApplicationUserId { get; set; }     

       [ForeignKey("ApplicationUserId")]
       public ApplicationUser ApplicationUser { get; set; }   

       public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }

        public int PolyclinicId { get; set; }

        [ForeignKey("PolyclinicId")]
        public Polyclinic LocationOfExamination { get; set; }

        public int SpecialtyId { get; set; }

        [ForeignKey("SpecialtyId")]
        public Specialty TypeOfExamination { get; set; }

        public int PrefferedTimeOfExaminationId { get; set; }

        [ForeignKey("PrefferedTimeOfExaminationId")]
        public PrefferedTimeOfExamination PrefferedTime { get; set; }

    }
}









