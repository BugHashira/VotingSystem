using VotingSystem.Models;

namespace VotingSystem.Data.Entities
{
    public class Vote : BaseEntity
    {
        public string UserId { get; set; }
        public Guid CandidateId { get; set; }
        public DateTime VoteTime { get; set; }
        public User User { get; set; }
        public Candidate Candidate { get; set; }
    }

}
