using VotingSystem.Data.Entities;
using VotingSystem.Dto;
using VotingSystem.Dto.Users;

namespace VotingSystem.Services.Interface
{
    public interface IUserService
    {
        Task<BaseResponseModel<bool>> AddUserAsync(CreateUserDto request);
        Task<BaseResponseModel<bool>> DeleteUserAsync(Guid id);
        Task<BaseResponseModel<IEnumerable<UserDto>>> GetAllUsersAsync();
        Task<BaseResponseModel<UserDto>> GetUserByIdAsync(Guid id);
        Task<BaseResponseModel<bool>> UpdateUserAsync(Guid id, UpdateUserDto request);
        Task<BaseResponseModel<bool>> UserRegistration(CreateUserDto request);
        Task<BaseResponseModel<bool>> UserLogin(UserLoginRequestDto request);
        User? GetUserName(string name);
        Task<BaseResponseModel<bool>> SignOutAsync();
    }
}
