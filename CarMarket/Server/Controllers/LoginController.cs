using CarMarket.BusinessLogic.User.Authentication.Models;
using CarMarket.BusinessLogic.User.Authentication.Service;
using CarMarket.Core.User.Domain;
using CarMarket.Core.User.JWT;
using CarMarket.Core.User.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarMarket.Server.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserService _userService;
        private readonly IAuthContainerModel _authContainerModel;
        private readonly IAuthService _authService;

        public LoginController(IUserService userService, IAuthContainerModel model, IAuthService authService)
        {
            _userService = userService;
            _authContainerModel = model;
            _authService = authService;
        }

        public IActionResult Login([FromBody] UserModel userModel)
        {

            TokenClass tokenClass = new TokenClass();
            var user = _userService.GetByEmail(userModel.Email);

            if(user is null)
            {
                tokenClass.TokenOrMessage = "Unauthorized user";
                return Ok(tokenClass);
            }

            bool credentials = user.Password.Equals(user.Password);
            if (!credentials)
            {
                tokenClass.TokenOrMessage = "Invalid password";
                return Ok(tokenClass);
            }

            tokenClass.TokenOrMessage = _authService.GenerateToken(_authContainerModel);
            return Ok(tokenClass);
        }
    }
}
