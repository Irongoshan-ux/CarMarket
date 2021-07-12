using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetAll()
        {
            return await _userService.GetAllAsync();
        }

        //TODO: Methods below don't work

        [HttpGet]
        [Route("{userId}")]
        public async Task<UserModel> Get(long userId)
        {
            return await _userService.GetAsync(userId);
        }

        [HttpPost]
        [Route("delete/{userId}")]
        public async Task Delete(long userId)
        {
            await _userService.DeleteAsync(userId);
        }

        [HttpPost]
        [Route("permission/change")]
        public async Task ChangeUserPermission(long userId, [FromQuery] Permission replaceablePermission, [FromQuery] Permission substitutePermission)
        {
            await _userService.ChangePermissionAsync(userId, replaceablePermission, substitutePermission);
        }

        [HttpPost]
        [Route("permission/add")]
        public async Task AddUserPermission(long userId, [FromBody] Permission[] permissions)
        {
            await _userService.AddPermissionAsync(userId, permissions);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] UserModel userModel)
        {
            var userId = await _userService.CreateAsync(userModel);

            if (userId == default)
            {
                return BadRequest(userModel + " is invalid");
            }

            return Ok(userModel);
        }
    }
}
