using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using CarMarket.Server.Infrastructure.Identification.Models;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Roles = "Admin")]
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

        [HttpPost("DeleteUser/{userId}")]
        public async Task DeleteUser(long userId)
        {
            await _userService.DeleteAsync(userId);
        }

        [HttpPost("ChangeUserPermission")]
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

        //UNUSED
        //[HttpPost]
        //[Route("authenticate")]
        //public async Task<IActionResult> Authenticate([FromBody] string email, [FromBody] string password)
        //{
        //    return BadRequest("This method has no implementation");

        //IAuthContainerModel model = GetJWTContainerModel("Admin", "admin@gmail.com");
        //IAuthService authService = new JWTService(model.SecretKey);

        //string token = authService.GenerateToken(model);

        //if (!authService.IsTokenValid(token))
        //    return new UnauthorizedResult();

        //List<Claim> claims = authService.GetTokenClaims(token).ToList();

        //var user = _userService.Authenticate(claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Name)).Value);




        //var user = _userService.Authenticate(email, password);

        //if (user is null)
        //    return BadRequest("Email or password is incorrect");

        //return await Ok(new
        //{
        //    Id = user.Id,
        //    Username = user.Username,
        //    FirstName = user.FirstName,
        //    LastName = user.LastName,
        //    Token = "fake-jwt-token"
        //});
        //}

        //private bool isLoggedIn()
        //{
        //    return false;
        //    //return Request.Headers.Authorization?.Parameter == "fake-jwt-token";
        //}

        private static JWTContainerModel GetJWTContainerModel(string role, string email)
        {
            return new JWTContainerModel()
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.Email, email)
                }
            };
        }
    }
}
