using System.Threading.Tasks;
using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Service;
using CarMarket.Server.Infrastructure.Identification.Models;
using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarMarket.Server.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class IdentificationController : Controller
    {
        private readonly ILogger<CarController> _logger;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IUserService _userService;

        public IdentificationController(
            IIdentityServerInteractionService interaction,
            IUserService userService,
            ILogger<CarController> logger)
        {
            _interaction = interaction;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public IActionResult Login(string returnUrl)
        {
            _logger.LogInformation($"Opened login page with returnUrl={returnUrl}");
            _logger.LogDebug("This is debug message");
            return View();
        }

        [HttpGet("[action]")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            model.ReturnUrl ??= Url.Content("https://localhost:5001");

            if (ModelState.IsValid)
            {
                var user = await _userService.AuthenticateAsync(model.Email, Utility.Encrypt(model.Password));

                if (user != null)
                {
                    await AuthorizeAsync(user);

                    _logger.LogInformation($"User {user.Email} has succesfully logged in.");

                    return Redirect(model.ReturnUrl);
                }

                ModelState.AddModelError("", "Invalid email or password");
            }

            _logger.LogInformation($"User {model.Email} failed to log in.");

            return View(model);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetByEmailAsync(model.Email);

                if (user != null)
                {
                    ModelState.AddModelError("", "Sorry, this email is already registered");
                    return View(model);
                }

                user = new UserModel
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash = Utility.Encrypt(model.Password)
                };

                await _userService.CreateAsync(user);

                await _userService.AddUserToRoleAsync(user, "User");

                await AuthorizeAsync(user);

                return Redirect("https://localhost:5001");
            }
            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            await HttpContext.SignOutAsync();

            Response.Cookies.Delete(".AspNetCore.Identity.Application");

            if (logout.PostLogoutRedirectUri is null)
                return Redirect("https://localhost:5001");

            return Redirect(logout.PostLogoutRedirectUri);
        }

        private async Task AuthorizeAsync(UserModel user)
        {
            user.PasswordHash = Utility.Encrypt(user.PasswordHash);

            await HttpContext.SignInAsync(new IdentityServerUser(user.Email));
        }
    }
}