using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.Interfaces;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Repositories
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly TaskManagementContext _context;

        public ActivityLogRepository(TaskManagementContext context)
        {
            _context = context;
        }

        public async Task AddLog(ActivityLog log)
        {
            await _context.ActivityLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}
