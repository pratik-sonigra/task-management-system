using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.Interfaces;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TaskManagementContext _context;

        public RoleRepository(TaskManagementContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleById(int roleId)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId);
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
        }
    }
}
