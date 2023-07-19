using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using WorkflowAutomation.Server.Models;
using WorkflowAutomation.Shared.Identity;
using System.Data;

namespace WorkflowAutomation.Server.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountsController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterDto dto)
        {
            var newUser = new ApplicationUser { UserName = dto.Email, Email = dto.Email };
            var result = await _userManager.CreateAsync(newUser, dto.Password);
            var allRoles = _roleManager.Roles.ToList();
            var role = allRoles.FirstOrDefault(r => r.NormalizedName == "ÇÀÐÅÃÈÑÒÐÈÐÎÂÀÍÍÛÉ ÏÎËÜÇÎÂÀÒÅËÜ").Name;
            await _userManager.AddToRoleAsync(newUser, role);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return Ok(new RegisterResult { Successful = false, Errors = errors });
            }

            return Ok(new RegisterResult { Successful = true });
        }
    }
}