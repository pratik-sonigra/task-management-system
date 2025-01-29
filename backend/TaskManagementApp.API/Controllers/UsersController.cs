using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata.Ecma335;
using TaskManagement.Application.Interfaces;
using TaskManagement.Contracts.DTOs;
using TaskManagement.Domain.Models;
using TaskManagement.Shared.Authentication;

namespace TaskManagementApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtTokenHelper _jwtTokenHelper;
        private readonly JWTConfigurations _jwtConfigurations;

        public UsersController(IUserService userService, JwtTokenHelper jwtTokenHelper, IOptions<JWTConfigurations> jwtConfigurations)
        {
            _userService = userService;
            _jwtTokenHelper = jwtTokenHelper;
            _jwtConfigurations = jwtConfigurations.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            var user = new User
            {
                Username = request.Username,
                PasswordHash = request.Password, // Password will be hashed in the service layer
                Email = request.Email,
                RoleId = request.RoleId
            };

            var success = await _userService.RegisterUser(user);
            if (!success)
            {
                return BadRequest(new { message = "Username already exists." });
            }
            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var user = await _userService.ValidateUser(request.Username, request.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }

            // Generate JWT Token
            var token = _jwtTokenHelper.GenerateToken(user);

            return Ok(new
            {
                Token = token,
                ExpiresIn = _jwtConfigurations.ExpiresInMinutes
            });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            var result = users.Select(u => new UserResponse
            {
                UserId = u.UserId,
                Username = u.Username
            });

            return Ok(result);
        }
    }
}
