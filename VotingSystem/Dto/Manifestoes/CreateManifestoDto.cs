using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Dto.Manifestoes
{
    public class CreateManifestoDto
    {
        [Required(ErrorMessage = "Candidate Id is required")]
        public Guid CandidateId { get; set; }

        [Required(ErrorMessage = "Manifesto note is required")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "Manifesto note is required")]
        public string FileExtension { get; set; }

        [Required(ErrorMessage = "Manifesto note is required")]
        public byte[] ManifestoNote { get; set; }
    }
}