using VotingSystem.Data;
using VotingSystem.Data.Entities;
using VotingSystem.Services.Interface;
using VotingSystem.Data.Dto.User;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Data.Dto.Users;

namespace VotingSystem.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(x => x.College)
                .Include(x => x.Department)
                .Select(x => new UserDto
                {
                    Id = Guid.Parse(x.Id),
                    MatricNumber = x.MatricNumber,
                    CollegeName = x.College.CollegeName,
                    CollegeId = x.CollegeId,
                    DepartmentName = x.Department.DepartmentName,
                    DepartmentId = x.DepartmentId
                }).ToListAsync();
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(x => x.College)
                .Include(x => x.Department)
                .Where(x => x.Id.Equals(id))
                .Select(x => new UserDto
                {
                    Id = Guid.Parse(x.Id),
                    MatricNumber = x.MatricNumber,
                    CollegeName = x.College.CollegeName,
                    CollegeId = x.CollegeId,
                    DepartmentName = x.Department.DepartmentName,
                    DepartmentId = x.DepartmentId
                }).FirstOrDefaultAsync();
        }

        public async Task AddUserAsync(CreateUserDto request)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                MatricNumber = request.MatricNumber,
                CollegeId = request.CollegeId,
                DepartmentId = request.DepartmentId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(Guid id, UpdateUserDto request)
        {
            var userExist = await _context.Users.Where(x => Guid.Parse(x.Id) == id).FirstOrDefaultAsync();
            if (userExist != null)
            {
                userExist.MatricNumber = request.MatricNumber;
                userExist.CollegeId = request.CollegeId;
                userExist.DepartmentId = request.DepartmentId;

                _context.Users.Update(userExist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
