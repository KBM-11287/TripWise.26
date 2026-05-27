using TripWise.Api.DTOs.Auth;
using TripWise.Api.Helpers;
using TripWise.Api.Models;
using TripWise.Api.Repositories;

namespace TripWise.Api.Services
{
    public class AuthService
    {
        private readonly IUserRepository _users;
        private readonly JwtService _jwt;
        private readonly PasswordHasher _hasher;

        public AuthService(IUserRepository users, JwtService jwt, PasswordHasher hasher)
        {
            _users = users;
            _jwt = jwt;
            _hasher = hasher;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            // Check if user already exists
            var existingUser = await _users.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new AuthResponse
                {
                    ErrorMessage = "Email is already registered."
                };
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = PasswordHasher.Hash(request.Password)
            };

            await _users.CreateAsync(user);

            return new AuthResponse
            {
                Token = _jwt.GenerateToken(user),
                Email = user.Email,
                Name = user.Name,
                UserId = user.Id
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _users.GetByEmailAsync(request.Email);

            if (user == null)
                return null;

            if(!PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
                return null;

            return new AuthResponse
            {
                Token = _jwt.GenerateToken(user),
                Email = user.Email,
                Name = user.Name,
                UserId = user.Id
            };
        }
    }
}
