using Application.DTOs.Auth;
using Application.Services.Interface.IAuth;
using Domain.Entities.User;  // Assuming ApplicationUser is in this namespace
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Implementation.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;  // Change to ApplicationUser
        private readonly SignInManager<ApplicationUser> _signInManager;  // Change to ApplicationUser
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<ApplicationUser> userManager,  // Change to ApplicationUser
            SignInManager<ApplicationUser> signInManager,  // Change to ApplicationUser
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<(bool Succeeded, string Message)> RegisterAsync(RegisterModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Ensure the "PendingApproval" role exists (done in the role seeding step in Program.cs)
                await _userManager.AddToRoleAsync(user, "PendingApproval");
                return (true, "Thank you for registering. Please wait for HR's response.");
            }

            string errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return (false, errors);
        }


        public async Task<(bool Succeeded, string Token)> LoginAsync(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                var token = GenerateJwtToken(model.Email);
                return (true, token);
            }

            return (false, null);
        }


        public string GenerateJwtToken(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, email)
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
