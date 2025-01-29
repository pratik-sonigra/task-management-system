using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.Interfaces;

namespace TaskManagement.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _roleRepository.GetAllRoles();
        }

        public async Task<Role> GetRoleById(int roleId)
        {
            return await _roleRepository.GetRoleById(roleId);
        }
    }
}
