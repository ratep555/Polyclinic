using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Publication1 : BaseEntity
    {
        public string PublicationAuthorsTitleDate { get; set; }
    }
}