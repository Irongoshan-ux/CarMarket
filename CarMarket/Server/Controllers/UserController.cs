using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        [HttpPost]
        public IActionResult Create([FromBody] UserModel userModel)
        {
            if (!_userService.Create(userModel))
                return BadRequest(userModel + " is invalid");

            return Ok(userModel);
        }
    }
}
