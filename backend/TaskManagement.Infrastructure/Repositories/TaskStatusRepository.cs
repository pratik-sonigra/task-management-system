using Microsoft.EntityFrameworkCore;
using TaskManagement.Infrastructure.Interfaces;
using TaskManagement.Infrastructure.Persistence;
using TaskStatus = TaskManagement.Domain.Models.TaskStatus;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskStatusRepository : ITaskStatusRepository
    {
        private readonly TaskManagementContext _context;

        public TaskStatusRepository(TaskManagementContext context)
        {
            _context = context;
        }

        public async Task<List<TaskStatus>> GetAllStatuses()
        {
            return await _context.TaskStatuses.ToListAsync();
        }

        public async Task<TaskStatus> GetStatusById(int statusId)
        {
            return await _context.TaskStatuses.FirstOrDefaultAsync(ts => ts.StatusId == statusId);
        }

        public async Task<TaskStatus> GetStatusByName(string statusName)
        {
            return await _context.TaskStatuses.FirstOrDefaultAsync(ts => ts.StatusName == statusName);
        }
    }
}
