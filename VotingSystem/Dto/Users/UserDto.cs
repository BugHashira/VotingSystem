using VotingSystem.Data.Entities;

namespace VotingSystem.Dto.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string MatricNumber { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Guid CollegeId { get; set; }
        public string CollegeName { get; set; }
    }


    public class UserLoginRequestDto
    {
        public string MatricNumber { get; set; }
        public string Password { get; set; }
    }

    public class UserResetPasswordRequestDto
    {
        public string Email { get; set; }
    }

    public class UserChangePasswordRequestDto
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
