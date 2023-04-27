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
    public class GenerateDataController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GenerateDataController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet("Generate")]
        public async Task Generate()
        {

            var allRoles = _roleManager.Roles.ToList();

            for (int i = 0; i < 2; i++) {
                var username = "adminUser" + i + "@mail.ru";
                var newUser = new ApplicationUser { UserName = username, Email = username };
                var result = await _userManager.CreateAsync(newUser, username);
                await _userManager.AddToRoleAsync(newUser, allRoles.FirstOrDefault(r => r.NormalizedName == "¿ƒÃ»Õ»—“–¿“Œ–").Name);
            }
        }
    }
}