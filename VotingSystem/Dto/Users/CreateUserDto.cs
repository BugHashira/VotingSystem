using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Dto.Users
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "MatricNumber is required")]
        public string MatricNumber { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "CollegeId is required")]
        public Guid CollegeId { get; set; }

        [Required(ErrorMessage = "DepartmentId is required")]
        public Guid DepartmentId { get; set; }
    }
}