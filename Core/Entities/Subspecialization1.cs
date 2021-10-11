using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Subspecialization1 : BaseEntity
    {
        public string SubspecializationName { get; set; }

        public int Specialization1Id { get; set; }
        [ForeignKey("Specialization1Id")]
        public Specialization1 Specialization { get; set; }
    }
}