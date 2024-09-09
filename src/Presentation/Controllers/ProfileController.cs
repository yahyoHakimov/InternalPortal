using Application.DTOs.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var profile = new ProfileModel
            {
                Email = user.Email,
                // Fetch and return more user data here
            };

            return Ok(profile);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            // Update user information
            user.Email = model.Email;
            // Update other fields here

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok("Profile updated successfully");
            }

            return BadRequest(result.Errors);
        }
    }

}
