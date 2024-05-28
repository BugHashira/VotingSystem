namespace VotingSystem.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public ICollection<Position>? Positions { get; set; }
    }
}




