using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Data.Dto.Candidates
{
    public class CreateCandidateDto
    {
        [Required(ErrorMessage = "Position Id is required")]
        public Guid PositionId { get; set; }
        [Required(ErrorMessage = "Level is required")]
        public string Level { get; set; }
        [Required(ErrorMessage = "Election Id is required")]
        public Guid ElectionId { get; set; }
        [Required(ErrorMessage = "Matric Number is required")]
        public string MatricNumber { get; set; }
        [Required(ErrorMessage = "Candidate name is required")]
        public string CandidateName { get; set; }
    }
}
