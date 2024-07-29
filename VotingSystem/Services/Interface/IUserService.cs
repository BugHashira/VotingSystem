using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Data.Dto.Users;
using VotingSystem.Data.Dto.User;

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
