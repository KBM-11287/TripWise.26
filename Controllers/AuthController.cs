using Microsoft.AspNetCore.Mvc;
using TripWise.Api.Models;
using TripWise.Api.Repositories;
using Asp.Versioning;
using TripWise.Api.Helpers;
using TripWise.Api.DTOs.Auth;

namespace TripWise.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _users;
        private readonly JwtService _jwt;

        public AuthController(IUserRepository userRepository, JwtService jwt)
        {
            _users = userRepository;
            _jwt = jwt;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest dto)
        {
            var existingUser = await _users.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                return BadRequest("User with this email already exists.");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = PasswordHasher.Hash(dto.Password),
            };

            await _users.CreateAsync(user);
            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest dto)
        {
            var user = await _users.GetByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized("Invalid email credentials.");
            if (user.PasswordHash != PasswordHasher.Hash(dto.Password))
                return Unauthorized("Invalid password.");

            var token = _jwt.GenerateToken(user);

            return Ok(new { token });
        }
    }
}
