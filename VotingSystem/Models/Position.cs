namespace VotingSystem.Models
{
    public class Position
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int CollegeId { get; set; }
        public College College { get; set; }
        public int SugId { get; set; }
        public Sug Sug { get; set; }
        public ICollection<Candidate> Candidates { get; set; }
    }
}

