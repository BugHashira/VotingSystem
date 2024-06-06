namespace VotingSystem.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        public int PositionId { get; set; }
        public string CandidateName { get; set; }
        public Position Position { get; set; }
    }

}
