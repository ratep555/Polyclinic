using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Department : BaseEntity
    {
        public string DepartmentName { get; set; }
    }
}