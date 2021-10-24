namespace Core.Dtos
{
    public class Doctor1Dto
    {
        public int Id { get; set; }
         public int ApplicationUserId { get; set; }            
        public string Name { get; set; }
        public string Resume { get; set; }
        public double AverageVote { get; set; }
        public int UserVote { get; set; }
    }
}