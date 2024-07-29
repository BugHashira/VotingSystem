using VotingSystem.Dto;
using VotingSystem.Dto.Users;

namespace VotingSystem.Services.Interface
{
    public interface IUserService
    {
        Task<BaseResponseModel<bool>> AddUserAsync(CreateUserDto request);
        Task<BaseResponseModel<bool>> DeleteUserAsync(int id);
        Task<BaseResponseModel<IEnumerable<UserDto>>> GetAllUsersAsync();
        Task<BaseResponseModel<UserDto>> GetUserByIdAsync(string id);
        Task<BaseResponseModel<bool>> UpdateUserAsync(Guid id, UpdateUserDto request);
    }
}
