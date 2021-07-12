using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

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
        public IEnumerable<UserModel> GetAll()
        {
            return _userService.GetAllAsync();
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserModel userModel)
        {
            if (!_userService.CreateAsync(userModel))
                return BadRequest(userModel + " is invalid");


            return Ok(userModel);
        }
    }
}
