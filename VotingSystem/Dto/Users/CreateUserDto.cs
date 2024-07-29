using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Dto.Users
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "MatricNumber is required")]
        public Guid MatricNumber { get; set; }

        [Required(ErrorMessage = "CollegeId is required")]
        public Guid CollegeId { get; set; }

        [Required(ErrorMessage = "DepartmentId is required")]
        public Guid DepartmentId { get; set; }
    }
}