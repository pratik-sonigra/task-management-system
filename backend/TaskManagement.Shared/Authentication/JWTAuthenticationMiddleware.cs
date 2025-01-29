using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TaskManagement.Contracts.DTOs;

namespace TaskManagement.Shared.Authentication
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JWTConfigurations _jwtConfiguration;

        public JwtAuthenticationMiddleware(RequestDelegate next, IOptions<JWTConfigurations> jwtConfiguration)
        {
            _next = next;
            _jwtConfiguration = jwtConfiguration.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                AttachUserToContext(context, token);
            }

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var key = Encoding.UTF8.GetBytes(_jwtConfiguration.Key);

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _jwtConfiguration.Issuer,
                    ValidAudience = _jwtConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero // Optional: Eliminate clock skew
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                context.User = principal; // Attach validated user to the HttpContext
            }
            catch
            {
                // Do nothing if the token is invalid
                // User will not be attached to the context, and access to secure endpoints will fail
            }
        }
    }
}
