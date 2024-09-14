using Domain.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;  // Assume ApplicationUser is your custom identity user class

        public RolesController(RoleManager<IdentityRole<int>> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // Get all available roles in the system
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles);  // Returns all roles from the system
        }

        // Get roles for the authenticated user
        [HttpGet("user-roles")]
        public async Task<IActionResult> GetUserRoles()
        {
            // Get the currently authenticated user
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            // Retrieve the roles of the user
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);  // Returns the roles of the authenticated user
        }
    }
}
