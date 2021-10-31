namespace Core.Entities
{
    public class DoctorSpecialization2
    {
        public int Doctor1Id { get; set; }       
        public Doctor1 Doctor { get; set; }
        
        public int Specialization1Id { get; set; }
        public Specialization1 Specialization { get; set; }
    }
}