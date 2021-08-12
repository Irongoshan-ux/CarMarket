using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace CarMarket.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
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
        public async Task DeleteUser(string userId)
        {
            await _userService.DeleteAsync(userId);
        }

        [HttpPost("ChangeUserPermission")]
        public async Task ChangeUserPermission(string userId, [FromQuery] Permission replaceablePermission, [FromQuery] Permission substitutePermission)
        {
            await _userService.ChangePermissionAsync(userId, replaceablePermission, substitutePermission);
        }

        [HttpPost("AddUserPermission")]
        public async Task AddUserPermission(string userId, [FromBody] Permission[] permissions)
        {
            await _userService.AddPermissionAsync(userId, permissions);
        }

        [HttpDelete("DeleteUserPermission")]
        public async Task DeleteUserPermission(string userId, [FromBody] Permission permission)
        {
            await _userService.DeletePermissionAsync(userId, permission);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> Create([FromBody] UserModel userModel)
        {
            userModel.PasswordHash = EncryptPassword(userModel.PasswordHash);

            var userId = await _userService.CreateAsync(userModel);

            if (userId == default)
                return BadRequest(userModel + " is invalid");

            return Ok(userModel);
        }

        [HttpPut("UpdateUser/{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, UserModel user)
        {
            if (userId != user.Id)
            {
                return BadRequest();
            }

            await _userService.UpdateUser(userId, user);

            return NoContent();
        }

        private string EncryptPassword(string password) => Utility.Encrypt(password);
    }
}
