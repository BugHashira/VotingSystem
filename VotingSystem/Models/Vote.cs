namespace VotingSystem.Models
{
    public class Vote
    {
        public int VoteId { get; set; }
        public int PositionId { get; set; }
        public int UserId { get; set; }
        public int CandidateId { get; set; }
        public DateTime VoteTime { get; set; }
        public Position? Position { get; set; }
        public User? User { get; set; }
        public Candidate? Candidate { get; set; }
    }

}
