using VotingSystem.Data.Dto.Candidates;

namespace VotingSystem.Data.Dto.Manifestoes
{
    public class ManifestoDto
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string ManifestoNote { get; set; }
        public string CandidateName { get; set; }
    }
}
