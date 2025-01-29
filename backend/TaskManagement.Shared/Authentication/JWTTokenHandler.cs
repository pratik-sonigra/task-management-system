using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Contracts.DTOs;
using TaskManagement.Domain.Models;

namespace TaskManagement.Shared.Authentication
{
    public class JwtTokenHelper
    {
        private readonly JWTConfigurations _jwtConfigurations;

        public JwtTokenHelper(IOptions<JWTConfigurations> jwtConfigurations)
        {
            _jwtConfigurations = jwtConfigurations.Value;
        }

        public string GenerateToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_jwtConfigurations.Key);
            var issuer = _jwtConfigurations.Issuer;
            var audience = _jwtConfigurations.Audience;
            var expiresInMinutes = _jwtConfigurations.ExpiresInMinutes;

            // Claims
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.RoleName) // Add user role
            };

            var keyCredentials = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(keyCredentials, SecurityAlgorithms.HmacSha256);

            // Generate JWT token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
