using System.Threading.Tasks;
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
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly UserManager<ApplicationUser> _userManager;

        public IdentificationController(
            IIdentityServerInteractionService interaction
            //SignInManager<ApplicationUser> signInManager,
            //UserManager<ApplicationUser> userManager
            )
        {
            _interaction = interaction;
            //_signInManager = signInManager;
            //_userManager = userManager;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Login(string returnUrl)
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please. Validate your credentials and try again.");
                return View(model);
            }

            if (model.Email != "admin@gmail.com")
            {
                ModelState.AddModelError("", "User not found");
                return View(model);
            }

            await HttpContext.SignInAsync(new IdentityServerUser(model.Email));

            //var user = await _userManager.FindByNameAsync(model.UserName);
            //if (user == null)
            //{
            //    ModelState.AddModelError("UserName", "User not found");
            //    return View(model);
            //}

            //var signResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            //if (!signResult.Succeeded)
            //{
            //    ModelState.AddModelError("", "Something went wrong");
            //    return View(model);
            //}

            return Redirect(model.ReturnUrl);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            await HttpContext.SignOutAsync();
            //await _signInManager.SignOutAsync();
            return Redirect(logout.PostLogoutRedirectUri);
        }
    }
}