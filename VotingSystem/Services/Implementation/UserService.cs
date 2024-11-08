using VotingSystem.Data;
using VotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Services.Interface;
using VotingSystem.Dto.Users;
using VotingSystem.Dto;
using Microsoft.AspNetCore.Identity;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace VotingSystem.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly INotyfService _notyfService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(ApplicationDbContext context, UserManager<User> userManager,
            SignInManager<User> signInManager, INotyfService notyfService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _notyfService = notyfService;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<BaseResponseModel<UserDto>> GetUserByIdAsync(Guid id)
        {
            try
            {

                var user = await _context.Users
                    .Include(x => x.College)
                    .Include(x => x.Department)
                    .Where(x => x.Id == id.ToString())
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
                var userExist = await _context.Users.FirstOrDefaultAsync(x => x.Id == id.ToString());

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

        public async Task<BaseResponseModel<bool>> DeleteUserAsync(Guid id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id.ToString());

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

        public async Task<BaseResponseModel<bool>> UserLogin(UserLoginRequestDto request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.MatricNumber);

                var result = await _signInManager
                    .PasswordSignInAsync(user!.UserName!, request.Password, false, lockoutOnFailure: false);

                if (result.Succeeded)
                {

                    _notyfService.Success("You're Logged in successfully");
                    return new BaseResponseModel<bool> { IsSuccessful = true, Message = "You're Logged in successfully" };
                }
                _notyfService.Error("Invald Login Attempt");
                return new BaseResponseModel<bool> { IsSuccessful = true, Message = "Invald Login Attempt" };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool> { IsSuccessful = false, Message = "Invald Login Attempt" };
            }
        }

        public async Task<BaseResponseModel<bool>> UserRegistration(CreateUserDto request)
        {
            try
            {
                var existingUser = await _userManager.Users.SingleOrDefaultAsync(u => u.MatricNumber == request.MatricNumber);
                if (existingUser != null)
                {
                    _notyfService.Warning("User already exist!");
                    return new BaseResponseModel<bool> { IsSuccessful = false, Message = "User already exist!" };
                }

                var user = new User
                {
                    UserName = request.MatricNumber,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Id = Guid.NewGuid().ToString(),
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    _notyfService.Error("User creation failed");
                    return new BaseResponseModel<bool> { IsSuccessful = false, Message = "User creation failed" };
                }

                var addRole = await _userManager.AddToRoleAsync(user, "Libarian");

                if (!addRole.Succeeded)
                {
                    _notyfService.Error("Add user role failed");
                    return new BaseResponseModel<bool> { IsSuccessful = false, Message = "User creation failed" };
                }


                _notyfService.Success("Registration was successful");
                await _signInManager.SignInAsync(user, isPersistent: false);

                return new BaseResponseModel<bool> { IsSuccessful = true, Message = "Registration was successful." };

            }
            catch (Exception ex)
            {

                return new BaseResponseModel<bool> { IsSuccessful = false, Message = "An error occured" };
            }
        }

        public async Task<BaseResponseModel<bool>> SignOutAsync()
        {
            await _signInManager.SignOutAsync();
            return new BaseResponseModel<bool> { IsSuccessful = true, Message = "Sign out successfully." };
        }

        public User? GetUserName(string name)
        {
            var user = _context.Users.Where(x => x.UserName == name).FirstOrDefault();

            if (user != null)
            {
                return user;
            }

            return null;
        }

    }
}
