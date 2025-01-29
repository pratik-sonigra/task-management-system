using TaskManagement.Domain.Models;

namespace TaskManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUser(User user); // Registers a new user
        Task<User> ValidateUser(string username, string password); // Validates login credentials
        Task<User> GetUserById(int userId); // Retrieves user details by ID
        Task<List<User>> GetAllUsers();
    }
}
