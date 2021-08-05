using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using CarMarket.Server.Infrastructure.Identification.Models;
using Microsoft.AspNetCore.Authorization;
using CarMarket.Core.Car.Domain;

namespace CarMarket.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    //[Authorize(Roles = "Admin")]
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
        public async Task<UserModel> GetUser(long userId)
        {
            return await _userService.GetAsync(userId);
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task DeleteUser(long userId)
        {
            await _userService.DeleteAsync(userId);
        }

        [HttpPost("ChangeUserPermission")]
        [Authorize(Policy = "")]
        public async Task ChangeUserPermission(long userId, [FromQuery] Permission replaceablePermission, [FromQuery] Permission substitutePermission)
        {
            await _userService.ChangePermissionAsync(userId, replaceablePermission, substitutePermission);
        }

        [HttpPost("AddUserPermission")]
        public async Task AddUserPermission(long userId, [FromBody] Permission[] permissions)
        {
            await _userService.AddPermissionAsync(userId, permissions);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> Create([FromBody] UserModel userModel)
        {
            userModel.Password = EncryptPassword(userModel.Password);

            var userId = await _userService.CreateAsync(userModel);

            if (userId == default)
                return BadRequest(userModel + " is invalid");

            return Ok(userModel);
        }

        [HttpPut("UpdateUser/{userId}")]
        public async Task<IActionResult> UpdateUser(long userId, UserModel user)
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
