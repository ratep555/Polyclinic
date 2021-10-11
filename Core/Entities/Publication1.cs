using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Publication1 : BaseEntity
    {
        public int? Doctor1Id { get; set; }
        [ForeignKey("Doctor1Id")]
        public Doctor1 Doctor { get; set; }
        public string PublicationName { get; set; }
        public string PublishingYear { get; set; }
    }
}