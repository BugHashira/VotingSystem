﻿using Microsoft.AspNetCore.Mvc;
using VotingSystem.Dto.Users;
using VotingSystem.Services.Interface;
using System.Threading.Tasks;
using System;

namespace VotingSystem.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("create-user")]
        public IActionResult CreateUser()
        {
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
            return RedirectToAction("CreateUser");
        }

        [HttpGet("edit-user/{id}")]
        public async Task<IActionResult> EditUser(string id)
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
        public async Task<IActionResult> UserDetail(string id)
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
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Users");
            }

            return RedirectToAction("Users");
        }
    }
}