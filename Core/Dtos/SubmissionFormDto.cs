using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class SubmissionFormDto
    {
        public int Id { get; set; }
         public int ApplicationUserId { get; set; }     


        [DataType(DataType.Date)]
        public DateTime PrefferedDateOfExamination { get; set; }
        public bool Status { get; set; }
        public string Phone { get; set; }


        public int PolyclinicId { get; set; }

        public int DepartmentId { get; set; }

        public int PrefferedTimeOfExaminationId { get; set; }

    }
}