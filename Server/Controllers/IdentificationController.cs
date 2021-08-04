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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarMarket.Server.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class IdentificationController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IUserService _userService;
        private readonly UserManager<UserModel> _userManager;

        public IdentificationController(
            IIdentityServerInteractionService interaction,
            UserManager<UserModel> userManager,
            IUserService userService
            )
        {
            _interaction = interaction;
            _userService = userService;
            _userManager = userManager;
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
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.AuthenticateAsync(model.Email, EncryptPassword(model.Password));

                if (user != null)
                {
                    await AuthorizeAsync(user);

                    await HttpContext.SignInAsync(new IdentityServerUser(user.Email));

                    return Redirect(model.ReturnUrl);
                }

                ModelState.AddModelError("", "Invalid email or password");
            }

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
                    ModelState.AddModelError("", "Invalid email or password");
                    return View();
                }

                user = new UserModel { Email = model.Email, PasswordHash = EncryptPassword(model.Password) };
                Role userRole = await _userService.GetUserRoleAsync("user");

                await _userService.CreateAsync(user);

                await AuthorizeAsync(user);
            }
            return Redirect("https://localhost:5001");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            await HttpContext.SignOutAsync();

            if (logout.PostLogoutRedirectUri is null) 
                return Redirect("https://localhost:5001");

            return Redirect(logout.PostLogoutRedirectUri);
        }

        private async Task AuthorizeAsync(UserModel user)
        {
            user.PasswordHash = EncryptPassword(user.PasswordHash);

            var claims = await GetUserClaimsAsync(user);
            
            var identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            //How does it work?

            //await HttpContext.SignInAsync(new IdentityServerUser(identity.NameClaimType));

            //await HttpContext.SignInAsync(new IdentityServerUser(identity.RoleClaimType));

            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            //await HttpContext.SignInAsync(new IdentityServerUser(identity.Claims.ToString()));
        }

        private async Task<IEnumerable<Claim>> GetUserClaimsAsync(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private string EncryptPassword(string password) => Utility.Encrypt(password);
    }
}