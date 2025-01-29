using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.Interfaces;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskManagementContext _context;

        public UserRepository(TaskManagementContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users
            .Include(u => u.Role) // Include Role for access to Role details
            .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users
            .Include(u => u.Role) // Include Role for access to Role details
            .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
