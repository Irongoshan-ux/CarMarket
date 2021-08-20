using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Service;
using CarMarket.Server.Infrastructure.Identification.Models;
using CarMarket.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdministrationController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserModel> _userManager;
        private readonly IUserService _userService;

        public AdministrationController(
            IUserService userService,
            RoleManager<IdentityRole> roleManager,
            UserManager<UserModel> userManager,
            ILogger<CarController> logger)
        {
            _userService = userService;
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (!await UserIsInAdminRole())
            {
                return BadRequest("Access denied");
            }

            if (ModelState.IsValid)
            {
                var identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return Ok(model);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return BadRequest($"Role {model.RoleName} already exists.");
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            if (!await UserIsInAdminRole())
            {
                return BadRequest("Access denied");
            }

            return Ok(_roleManager.Roles.ToList());
        }

        [HttpGet("GetUsersInRole")]
        public async Task<IActionResult> GetUsersInRole(string roleId)
        {
            if (!await UserIsInAdminRole())
            {
                return BadRequest("Access denied");
            }

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
            {
                return NotFound($"Role with id={roleId} not found");
            }

            var models = new List<UserRoleViewModel>();

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    var userRoleViewModel = new UserRoleViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName
                    };

                    models.Add(userRoleViewModel);
                }
            }

            return Ok(models);
        }

        [HttpPost("EditUsersInRole")]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            if (!await UserIsInAdminRole())
            {
                return BadRequest("Access denied");
            }

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
            {
                return NotFound($"Role with id={roleId} not found");
            }

            IdentityResult result = null;

            foreach (var userRoleModel in model)
            {
                var user = await _userManager.FindByIdAsync(userRoleModel.UserId);

                if ((userRoleModel.IsSelected) && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }

                if (!(userRoleModel.IsSelected) && (await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }

                else continue;
            }

            return Ok(result);
        }

        private async Task<bool> UserIsInAdminRole()
        {
            var currentUser = await GetCurrentUserAsync();

            return await _userManager.IsInRoleAsync(currentUser, "Admin");
        }

        private async Task<UserModel> GetCurrentUserAsync() => await UserHelper.GetCurrentUserAsync(_userService, HttpContext);
    }
}
