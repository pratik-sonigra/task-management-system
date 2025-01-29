using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Interfaces;
using TaskManagement.Contracts.DTOs;

namespace TaskManagementApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRoles();
            var response = roles.Select(r => new RoleResponse
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName
            });
            return Ok(response);
        }

        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRoleById(int roleId)
        {
            var role = await _roleService.GetRoleById(roleId);
            if (role == null)
            {
                return NotFound(new { message = $"Role with ID {roleId} not found." });
            }
            return Ok(new RoleResponse { RoleId = role.RoleId, RoleName = role.RoleName });
        }
    }
}
