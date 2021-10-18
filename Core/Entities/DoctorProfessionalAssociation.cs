using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class DoctorProfessionalAssociation : BaseEntity
    {
        public int Doctor1Id { get; set; }
        [ForeignKey("Doctor1Id")]
        public Doctor1 Doctor { get; set; }

        public int ProfessionalAssociationId { get; set; }
        [ForeignKey("ProfessionalAssociationId")]
        public ProfessionalAssociation ProfessionalAssociation { get; set; }
    }
}