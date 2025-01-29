using TaskManagement.Domain.Models;

namespace TaskManagement.Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetAllRoles(); // Retrieves all roles
        Task<Role> GetRoleById(int roleId); // Fetches a specific role
    }
}
