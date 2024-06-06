namespace VotingSystem.Models
{
    public class Sug
    {
        public int SugId { get; set; }
        public string SugName { get; set; }
        public ICollection<Position> Positions { get; set; }
    }

}
