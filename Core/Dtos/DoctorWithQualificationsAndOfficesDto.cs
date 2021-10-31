using System.Collections.Generic;

namespace Core.Dtos
{
    public class DoctorWithQualificationsAndOfficesDto
    {
        public Doctor1Dto Doctor { get; set; }
        public List<SpecializationDto> Specializations { get; set; }
        public List<SubspecializationDto> Subspecializations { get; set; }
        public List<ProfessionalAssociationDto> ProfessionalAssociations { get; set; }
        public List<PublicationDto> Publications { get; set; }
        public List<OfficeToReturnDto> Offices { get; set; }
    }
}