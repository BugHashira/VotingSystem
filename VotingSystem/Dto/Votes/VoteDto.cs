using VotingSystem.Data.Entities;

namespace VotingSystem.Dto.Votes
{
    public class VoteDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid CandidateId { get; set; }
        public DateTime VoteTime { get; set; }
        public string UserName { get; set; }
        public string CandidateName { get; set; }
    }
}
