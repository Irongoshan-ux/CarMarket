using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CarMarket.Server.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class IdentificationController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IUserService _userService;
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;

        public IdentificationController(
            IIdentityServerInteractionService interaction,
            UserManager<UserModel> userManager,
            IUserService userService,
            SignInManager<UserModel> signInManager,
            IConfiguration configuration
            )
        {
            _interaction = interaction;
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
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

                    //var identityUser = new IdentityServerUser(user.Email);

                    //await HttpContext.SignInAsync(new IdentityServerUser(user.Email));

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

            var claims = await GetClaimsAsync(user);

            //var identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
            //    ClaimsIdentity.DefaultRoleClaimType);

            var claim = claims.ElementAt(1);

            var identityUser = new IdentityServerUser(user.UserName)
            {
                AdditionalClaims = claims
            };

            //await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            await _userManager.AddClaimsAsync(user, claims);

            //await _signInManager.SignInWithClaimsAsync(user, false, claims); // throws an exception
            // Check https://stackoverflow.com/questions/46884549/identityserver4-sub-claim-is-missing

            //await _userManager.UpdateAsync(user);

            //await _signInManager.SignInAsync(user, isPersistent: false);

            //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

            // How to include role claim to jwt token and then use the Authorize attribute?
            // See https://weblog.west-wind.com/posts/2021/Mar/09/Role-based-JWT-Tokens-in-ASPNET-Core

            await HttpContext.SignInAsync(identityUser); // don't add the role claim to jwt token :(

            //await HttpContext.SignInAsync(new IdentityServerUser(identity.RoleClaimType));

            //await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
        }

        private async Task<ICollection<Claim>> GetClaimsAsync(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings.GetSection("validIssuer").Value,
                audience: _jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }

        private string EncryptPassword(string password) => Utility.Encrypt(password);
    }
}