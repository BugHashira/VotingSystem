using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Dto.Manifestoes
{
    public class UpdateManifestoDto
    {
        [Required(ErrorMessage = "Candidate Id is required")]
        public Guid CandidateId { get; set; }

        [Required(ErrorMessage = "Manifesto note is required")]
        public string ManifestoNote { get; set; }

        [Required(ErrorMessage = "Candidate Name is required")]
        public string CandidateName { get; set; }
    }
}
