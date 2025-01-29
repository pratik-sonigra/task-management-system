using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.Interfaces;

namespace TaskManagement.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _userRepository.GetUserById(userId);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<bool> RegisterUser(User user)
        {
            // Check if the username already exists
            if (await _userRepository.UserExists(user.Username))
            {
                return false; // Username is already taken
            }

            // Hash the user's password before storing it
            user.PasswordHash = HashPassword(user.PasswordHash);

            // Assign a default role (e.g., User) if no role is provided
            if (user.RoleId == 0)
            {
                user.RoleId = 2; // Assuming 2 is the "User" role ID
            }

            await _userRepository.AddUser(user);
            return true;
        }

        public async Task<User> ValidateUser(string username, string password)
        {
            // Retrieve the user by username
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                return null; // Invalid username or password
            }

            return user;
        }

        private string HashPassword(string password)
        {
            // Use BCrypt to hash the password
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            // Use BCrypt to verify the password against the stored hash
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
