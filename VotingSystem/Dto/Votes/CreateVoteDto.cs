using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Dto.Votes
{
    public class CreateVoteDto
    {
        [Required(ErrorMessage = "User Id is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Candidate Id is required")]
        public Guid CandidateId { get; set; }

        [Required(ErrorMessage = "Vote time is required")]
        public DateTime VoteTime { get; set; }
    }
}