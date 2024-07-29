using VotingSystem.Dto.Users;

namespace VotingSystem.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task AddUserAsync(CreateUserDto request);
        Task UpdateUserAsync(Guid id, UpdateUserDto request);
        Task DeleteUserAsync(int id);
    }
}
