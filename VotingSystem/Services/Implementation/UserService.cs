using VotingSystem.Data;
using VotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Services.Interface;
using VotingSystem.Dto.Users;
using VotingSystem.Dto;

namespace VotingSystem.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _context.Users
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

                if (users.Count > 0)
                {
                    return new BaseResponseModel<IEnumerable<UserDto>>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = users
                    };
                }

                return new BaseResponseModel<IEnumerable<UserDto>>()
                {
                    IsSuccessful = false,
                    Message = "No record found",
                    Data = users
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<IEnumerable<UserDto>>()
                {
                    IsSuccessful = false,
                    Message = "UserService : GetAllUsersAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<UserDto>> GetUserByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var userId))
                {
                    return new BaseResponseModel<UserDto>()
                    {
                        IsSuccessful = false,
                        Message = "Invalid ID format"
                    };
                }

                var user = await _context.Users
                    .Include(x => x.College)
                    .Include(x => x.Department)
                    .Where(x => x.Id.Equals(userId))
                    .Select(x => new UserDto
                    {
                        Id = Guid.Parse(x.Id),
                        MatricNumber = x.MatricNumber,
                        CollegeName = x.College.CollegeName,
                        CollegeId = x.CollegeId,
                        DepartmentName = x.Department.DepartmentName,
                        DepartmentId = x.DepartmentId
                    }).FirstOrDefaultAsync();

                if (user != null)
                {
                    return new BaseResponseModel<UserDto>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = user
                    };
                }

                return new BaseResponseModel<UserDto>
                {
                    IsSuccessful = false,
                    Message = "No record found"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<UserDto>()
                {
                    IsSuccessful = false,
                    Message = "UserService : GetUserByIdAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> AddUserAsync(CreateUserDto request)
        {
            try
            {
                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    MatricNumber = request.MatricNumber,
                    CollegeId = request.CollegeId,
                    DepartmentId = request.DepartmentId
                };

                await _context.Users.AddAsync(user);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new BaseResponseModel<bool>()
                    {
                        IsSuccessful = true,
                        Message = "Data created successfully",
                        Data = true
                    };
                }

                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "Create failed",
                    Data = false
                };
            }
            catch (Exception)
            {
                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "UserService : AddUserAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> UpdateUserAsync(Guid id, UpdateUserDto request)
        {
            try
            {
                var userExist = await _context.Users.Where(x => Guid.Parse(x.Id) == id).FirstOrDefaultAsync();

                if (userExist == null)
                    return new BaseResponseModel<bool>() { IsSuccessful = false, Message = "No record found", Data = false };

                userExist.MatricNumber = request.MatricNumber;
                userExist.CollegeId = request.CollegeId;
                userExist.DepartmentId = request.DepartmentId;

                _context.Users.Update(userExist);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new BaseResponseModel<bool>()
                    {
                        IsSuccessful = true,
                        Message = "Data updated successfully",
                        Data = true
                    };
                }

                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "Update failed",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "UserService : UpdateUserAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                    return new BaseResponseModel<bool>() { IsSuccessful = false, Message = "No record found", Data = false };

                _context.Users.Remove(user);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new BaseResponseModel<bool>()
                    {
                        IsSuccessful = true,
                        Message = "Data deleted successfully",
                        Data = true
                    };
                }

                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "Delete failed",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "UserService : DeleteUserAsync : Error Occurred:"
                };
            }
        }
    }
}
