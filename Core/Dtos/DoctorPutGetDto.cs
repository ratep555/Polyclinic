using System.Collections.Generic;
using Core.Entities;

namespace Core.Dtos
{
    public class DoctorPutGetDto
    {
        public Doctor1Dto Doctor { get; set; }
        public List<SpecializationDto> SelectedSpecializations { get; set; }
        public List<SpecializationDto> NonSelectedSpecializations { get; set; }
    }
}