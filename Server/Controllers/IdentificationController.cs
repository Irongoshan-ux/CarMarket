using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Service;
using CarMarket.Server.Infrastructure.Identification.Models;
using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarMarket.Server.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class IdentificationController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IUserService _userService;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly UserManager<ApplicationUser> _userManager;

        public IdentificationController(
            IIdentityServerInteractionService interaction,
            IUserService userService
            //SignInManager<ApplicationUser> signInManager,
            //UserManager<ApplicationUser> userManager
            )
        {
            _interaction = interaction;
            _userService = userService;
            //_signInManager = signInManager;
            //_userManager = userManager;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Login(string returnUrl)
        {
            return View();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Register(string returnUrl)
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.AuthenticateAsync(model.Email, EncryptPassword(model.Password));

                if (user != null)
                {
                    await Authenticate(user);

                    await HttpContext.SignInAsync(new IdentityServerUser(model.Email));

                    return Redirect(model.ReturnUrl);
                }

                ModelState.AddModelError("", "Invalid email or password");
            }

            return View(model);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetByEmailAsync(model.Email);

                if (user != null)
                {
                    ModelState.AddModelError("", "Invalid email or password");
                    return View();
                }

                user = new UserModel { Email = model.Email, Password = EncryptPassword(model.Password) };
                Role userRole = await _userService.GetUserRoleAsync("user");

                if (userRole != null)
                {
                    user.Role = userRole;
                }

                await _userService.CreateAsync(user);

                await Authenticate(user);
            }
            return Redirect("https://localhost:5001"); // Redirect(model.ReturnUrl);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            await HttpContext.SignOutAsync();
            return Redirect(logout.PostLogoutRedirectUri);
        }

        private async Task Authenticate(UserModel user)
        {
            user.Password = Utility.Encrypt(user.Password);

            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(new IdentityServerUser(id.NameClaimType));//, new ClaimsPrincipal(id));

            //await HttpContext.SignInAsync(new IdentityServerUser(user.Email));
        }

        private string EncryptPassword(string password) => Utility.Encrypt(password);
    }
}