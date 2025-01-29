using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.Interfaces;

namespace TaskManagement.Application.Services
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly IActivityLogRepository _activityLogRepository;

        public ActivityLogService(IActivityLogRepository activityLogRepository)
        {
            _activityLogRepository = activityLogRepository;
        }

        public async Task AddLog(ActivityLog log)
        {
            await _activityLogRepository.AddLog(log);
        }
    }
}
