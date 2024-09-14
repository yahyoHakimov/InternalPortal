using Application.DTOs.Auth;
using Application.Services.Interface.IAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (Succeeded, Message) = await _authService.RegisterAsync(model);

            if (Succeeded)
            {
                return Ok(Message);
            }

            ModelState.AddModelError("", Message);
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var (Succeeded, Token) = await _authService.LoginAsync(model);

            if (Succeeded)
            {
                return Ok(new { Token });
            }

            return Unauthorized();
        }

        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // This will return the authenticated user's ID
            return Ok(new { userId });
        }

        
    }

}
