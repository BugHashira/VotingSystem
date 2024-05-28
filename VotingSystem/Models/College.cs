namespace VotingSystem.Models
{
    public class College
    {
        public int CollegeId { get; set; }
        public string? CollegeName { get; set; }
        public ICollection<Position>? Positions { get; set; }
    }

}
