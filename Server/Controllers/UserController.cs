using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using CarMarket.Server.Services;
using Microsoft.AspNetCore.Identity;
using CarMarket.Core.Car.Domain;
using System.Linq;

namespace CarMarket.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly UserManager<UserModel> _userManager;

        public UserController(IUserService userService, UserManager<UserModel> userManager, ILogger<UserController> logger)
        {
            _userService = userService;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet("GetUsers")]
        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await _userService.GetAllAsync();
        }

        [HttpGet("GetUser/{userId}")]
        public async Task<UserModel> GetUser(string userId)
        {
            return await _userService.GetAsync(userId);
        }

        [HttpGet("GetUserByEmail")]
        public async Task<UserModel> GetUserByEmail(string email)
        {
            return await _userService.GetByEmailAsync(email);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string email)
        {
            try
            {
                var result = await _userService.SearchAsync(email);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("GetUsersByPage")]
        public async Task<IActionResult> GetUsersByPage(int skip = 0, int take = 5)
        {
            try
            {
                return Ok(await _userService.GetByPageAsync(skip, take));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (!await IsCurrentUserAdminAsync())
            {
                return BadRequest();
            }

            await _userService.DeleteAsync(userId);

            return NoContent();
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> Create([FromBody] UserModel userModel)
        {
            if (!await IsCurrentUserAdminAsync() || ((await _userService.GetByEmailAsync(userModel.Email)) is not null))
            {
                return BadRequest();
            }

            userModel.PasswordHash = EncryptPassword(userModel.PasswordHash);

            var userId = await _userService.CreateAsync(userModel);

            if (userId == default)
                return BadRequest(userModel + " is invalid");

            var createdUser = await _userService.GetAsync(userId);

            await _userService.AddUserToRoleAsync(createdUser, userModel.Role.Name);

            return Ok(createdUser);
        }

        [HttpPut("UpdateUser/{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, UserModel user)
        {
            if (userId != user.Id || !await IsCurrentUserAdminAsync() || await IsUserAdminAsync(user))
            {
                return BadRequest();
            }

            await _userService.UpdateUserAsync(userId, user);

            var updatedUser = await _userService.GetAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(updatedUser);

            if ((!string.IsNullOrWhiteSpace(user.Role.Name)) && (!userRoles.Contains(user.Role.Name)))
            {
                if (userRoles.Count > 0)
                {
                    await _userManager.RemoveFromRolesAsync(updatedUser, userRoles);
                }

                await _userService.AddUserToRoleAsync(updatedUser, user.Role.Name);
            }

            return NoContent();
        }

        private string EncryptPassword(string password) => Utility.Encrypt(password);

        private async Task<bool> IsCurrentUserAdminAsync()
        {
            var user = await GetCurrentUserAsync();

            return await IsUserAdminAsync(user);
        }

        private async Task<bool> IsUserAdminAsync(UserModel user) => await _userManager.IsInRoleAsync(user, "Admin");

        private async Task<UserModel> GetCurrentUserAsync() => await HttpUserHelper.GetCurrentUserAsync(_userService, HttpContext);
    }
}