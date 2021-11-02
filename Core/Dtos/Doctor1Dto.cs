using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Core.Dtos
{
    public class Doctor1Dto
    {
        public int Id { get; set; }
         public int ApplicationUserId { get; set; }            
        public string Name { get; set; }
        public string Resume { get; set; }
        public int? RateSum { get; set; }
        public int? Count { get; set; }
        public double? AverageVote { get; set; }
        public int UserVote { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? StartedPracticing { get; set; }

        public List<SpecializationDto> Specializations { get; set; }
        
        // ovo ne treba, just in case ostavljaš
      //  public ICollection<DoctorSpecialization2> DoctorSpecializations2 { get; set; }

    }
}



