namespace Core.Entities
{
    public class PolyclinicDepartment
    {
        public int PolyclinicId { get; set; }
        public Polyclinic Polyclinic { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}