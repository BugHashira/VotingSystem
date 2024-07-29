using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Data.Dto.User
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "MatricNumber is required")]
        public Guid MatricNumber { get; set; }

        [Required(ErrorMessage = "CollegeId is required")]
        public Guid CollegeId { get; set; }

        [Required(ErrorMessage = "DepartmentId is required")]
        public Guid DepartmentId { get; set; }
    }
}
