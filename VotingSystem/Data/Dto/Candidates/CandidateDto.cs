using VotingSystem.Data.Enums;

namespace VotingSystem.Data.Dto.Candidates
{
    public class CandidateDto
    {
        public Guid Id { get; set; }
        public Guid PositionId { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string Level { get; set; }
        public Guid ElectionId { get; set; }
        public string MatricNumber { get; set; }
        public string CandidateName { get; set; }
        public string PositionName { get; set; }
        public string ElectionName { get; set; }
    }
}
