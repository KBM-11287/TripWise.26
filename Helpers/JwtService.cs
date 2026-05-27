using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using TripWise.Api.Models;

namespace TripWise.Api.Helpers
{
    public class JwtService
    {
        private readonly string _secret;
        private readonly string _issuer;

        public JwtService(IConfiguration config)
        {
            _secret = config["Jwt:Secret"] ?? throw new ArgumentNullException("Jwt:Secret");
            _issuer = config["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer");
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(12),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
