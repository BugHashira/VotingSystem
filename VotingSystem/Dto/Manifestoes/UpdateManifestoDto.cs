using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Dto.Manifestoes
{
    public class UpdateManifestoDto
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Candidate Id is required")]
        public Guid CandidateId { get; set; }

        [Required(ErrorMessage = "Manifesto note is required")]
        public byte[] ManifestoNote { get; set; }
    }
}
