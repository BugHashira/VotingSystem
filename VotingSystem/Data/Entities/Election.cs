using VotingSystem.Models;

namespace VotingSystem.Data.Entities
{
    public class Election : BaseEntity
    {
        public string ElectionName { get; set; }
        public string DescriptionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Vote> Votes { get; set; } = new HashSet<Vote>();
    }
}
