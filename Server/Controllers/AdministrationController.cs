using CarMarket.Data.User.Domain;
using CarMarket.Server.Infrastructure.Identification.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserEntity> _userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<UserEntity> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
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

        [AllowAnonymous]
        [HttpGet("GetRoles")]
        public IList<IdentityRole> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }

        [AllowAnonymous]
        [HttpGet("GetUsersInRole")]
        public async Task<IActionResult> GetUsersInRole(string roleId)
        {
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
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
            {
                return NotFound($"Role with id={roleId} not found");
            }

            var result = new IdentityResult();

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
    }
}
