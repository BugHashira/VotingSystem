using VotingSystem.Data.Enums;
using VotingSystem.Models;

namespace VotingSystem.Data.Entities
{
    public class Candidate : BaseEntity
    { 
        public Guid PositionId { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string Level { get; set; }
        public Guid ElectionId { get; set; }
        public string MatricNumber { get; set; }
        public string CandidateName { get; set; }
        public Position Position { get; set; }
        public Election Election { get; set; }
    }

}
