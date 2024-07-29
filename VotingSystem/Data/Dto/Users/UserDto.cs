using VotingSystem.Data.Entities;

namespace VotingSystem.Data.Dto.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public Guid MatricNumber { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Guid CollegeId { get; set; }
        public string CollegeName { get; set; }
    }
}
