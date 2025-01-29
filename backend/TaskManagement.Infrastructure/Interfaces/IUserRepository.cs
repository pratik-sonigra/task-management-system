using TaskManagement.Domain.Models;

namespace TaskManagement.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int userId);
        Task<User> GetUserByUsername(string username);
        Task AddUser(User user);
        Task<bool> UserExists(string username);
        Task<List<User>> GetAllUsers();
    }
}
