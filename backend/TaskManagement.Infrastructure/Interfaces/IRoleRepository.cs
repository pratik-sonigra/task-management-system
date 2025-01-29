using TaskManagement.Domain.Models;

namespace TaskManagement.Infrastructure.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllRoles();
        Task<Role> GetRoleById(int roleId);
        Task<Role> GetRoleByName(string roleName);
    }
}
