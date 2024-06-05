using VotingSystem.Models;

namespace VotingSystem.Data.Entities
{
    public class Manifesto : BaseEntity
    {
        public Guid CandidateId { get; set; }
        public string ManifestoNote { get; set; }
        public Candidate Candidate { get; set; }
    }
}
