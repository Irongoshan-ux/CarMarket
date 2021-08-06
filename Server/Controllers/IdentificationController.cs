﻿using System.Threading.Tasks;
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
        private readonly UserManager<UserModel> _userManager;

        public IdentificationController(
            IIdentityServerInteractionService interaction,
            IUserService userService,
            UserManager<UserModel> userManager,
            ILogger<CarController> logger)
        {
            _interaction = interaction;
            _userService = userService;
            _userManager = userManager;
            _logger = logger;
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

                user = new UserModel { Email = model.Email, UserName = model.Email, PasswordHash = EncryptPassword(model.Password) };

                await _userService.CreateAsync(user);

                var result = await _userManager.AddToRoleAsync(user, "User"); // Description: "Optimistic concurrency failure, object has been modified."

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
           
            await HttpContext.SignInAsync(new IdentityServerUser(user.Email));
        }
        
        private string EncryptPassword(string password) => Utility.Encrypt(password);
    }
}