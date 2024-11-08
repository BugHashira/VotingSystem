using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VotingSystem.Dto.Users;
using VotingSystem.Services.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystem.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICollegeService _collegeService; // Assuming these services exist
        private readonly IDepartmentService _departmentService;

        public UserController(IUserService userService, ICollegeService collegeService, IDepartmentService departmentService)
        {
            _userService = userService;
            _collegeService = collegeService;
            _departmentService = departmentService;
        }

        [HttpGet("create-user")]
        public async Task<IActionResult> CreateUser()
        {
            // Get colleges from the service
            var collegeResponse = await _collegeService.GetAllCollegesAsync();
            var departmentResponse = await _departmentService.GetAllDepartmentsAsync();

            // Check if the responses were successful before proceeding
            if (collegeResponse.IsSuccessful && departmentResponse.IsSuccessful)
            {
                // Extract and convert the college data to a list of SelectListItems
                ViewBag.SelectColleges = collegeResponse.Data.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CollegeName
                }).ToList();

                // Extract and convert the department data to a list of SelectListItems
                ViewBag.SelectDepartments = departmentResponse.Data.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.DepartmentName
                }).ToList();
            }
            else
            {
                // Handle the error case if data is not retrieved successfully
                ViewBag.SelectColleges = new List<SelectListItem>();
                ViewBag.SelectDepartments = new List<SelectListItem>();

                // Optionally, add an error message
                ModelState.AddModelError(string.Empty, "Failed to load colleges or departments.");
            }

            return View();
        }


        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromForm] CreateUserDto request)
        {
            var result = await _userService.AddUserAsync(request);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Users");
            }

            // Reload dropdown lists if the form submission fails
            var collegeResponse = await _collegeService.GetAllCollegesAsync();
            var departmentResponse = await _departmentService.GetAllDepartmentsAsync();

            // Extract and convert the college data to a list of SelectListItems
            ViewBag.SelectColleges = collegeResponse.Data.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CollegeName
            }).ToList();

            // Extract and convert the department data to a list of SelectListItems
            ViewBag.SelectDepartments = departmentResponse.Data.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.DepartmentName
            }).ToList();

            return View();
        }

        [HttpGet("edit-user/{id}")]
        public async Task<IActionResult> EditUser(Guid id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return RedirectToAction("Users");
        }

        [HttpPost("edit-user/{id}")]
        public async Task<IActionResult> EditUser(UpdateUserDto request, Guid id)
        {
            var result = await _userService.UpdateUserAsync(id, request);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Users");
            }
            return RedirectToAction("EditUser", new { id = id });
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> UserDetail(Guid id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return RedirectToAction("Users");
        }

        [HttpGet("users")]
        public async Task<IActionResult> Users()
        {
            var result = await _userService.GetAllUsersAsync();

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return View(result.Data);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Users");
            }

            return RedirectToAction("Users");
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestDto request, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _userService.UserLogin(request);

                if (result.IsSuccessful)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                return View();
            }

            return View();
        }

        [HttpGet("register-user")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> Register(CreateUserDto request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UserRegistration(request);

                if (result.IsSuccessful)
                {
                    return RedirectToAction("Login", "Auth");
                }
                return View();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var result = await _userService.SignOutAsync();

            if (result.IsSuccessful)
                return RedirectToAction("Login", "Auth", new { returnUrl = "" });

            return View();
        }
    }
}
