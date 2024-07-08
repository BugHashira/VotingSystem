using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Data.Dto.Candidates
{
    public class CreateCandidateDto
    {
        [Required(ErrorMessage = "Position Id is requred")]
        public Guid PositionId { get; set; }
        [Required(ErrorMessage = "Level is requred")]
        public string Level { get; set; }
        [Required(ErrorMessage = "Election Id is requred")]
        public Guid ElectionId { get; set; }
        [Required(ErrorMessage = "Matri Number is requred")]
        public string MatricNumber { get; set; }
        [Required(ErrorMessage = "Candidate name is requred")]
        public string CandidateName { get; set; }
    }
}
