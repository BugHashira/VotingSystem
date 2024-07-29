using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Data.Dto.Manifesto
{
    public class CreateManifestoDto
    {
        [Required(ErrorMessage = "Candidate Id is required")]
        public Guid CandidateId { get; set; }

        [Required(ErrorMessage = "Manifesto note is required")]
        public string ManifestoNote { get; set; }
    }
}