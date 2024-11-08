namespace VotingSystem.Dto.Manifestoes
{
    public class ManifestoDto
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public byte[] ManifestoNote { get; set; }
        public string ManifestoFileName { get; set; }
        public string ManifestoNoteExtension { get; set; }
        public string CandidateName { get; set; }
    }
}
