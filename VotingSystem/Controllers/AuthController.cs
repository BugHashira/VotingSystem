using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VotingSystem.Data.Entities;
using VotingSystem.Dto.Users;
using VotingSystem.Services.Interface;

namespace VotingSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly SignInManager<User> signInManager;

       

        public AuthController(IUserService userService)
        {
            _userService = userService;
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
    }
}
