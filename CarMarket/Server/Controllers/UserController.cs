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

        [HttpPost]
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
