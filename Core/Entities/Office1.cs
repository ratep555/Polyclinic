using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace Core.Entities
{
    public class Office1 : BaseEntity
    {
        public int Doctor1Id { get; set; }
        [ForeignKey("Doctor1Id")]
        public Doctor1 Doctor { get; set; }

        public int? HospitalAffiliationId { get; set; }
        [ForeignKey("HospitalAffiliationId")]
        public HospitalAffiliation HospitalAffiliation { get; set; }
        
        public decimal InitialExaminationFee { get; set; }
        public decimal FollowUpExaminationFee { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public Point Location { get; set; }
        public string Picture { get; set; }
        public int? PhotoId { get; set; }
        [ForeignKey("PhotoId")]
        public Photo Photo { get; set; }

        public ICollection<Appointment1> Appointments { get; set; }
        // public ICollection<MedicalRecord> MedicalRecords { get; set; }
    }
}






