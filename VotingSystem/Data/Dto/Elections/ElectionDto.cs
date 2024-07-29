using VotingSystem.Data.Dto.Votes;
using VotingSystem.Data.Entities;

namespace VotingSystem.Data.Dto.Elections
{
    public class ElectionDto
    {
        public Guid Id { get; set; }
        public string ElectionName { get; set; }
        public string DescriptionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<VoteDto> Votes { get; set; } = new HashSet<VoteDto>();
    }
}
