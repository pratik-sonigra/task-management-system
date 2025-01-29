using TaskManagement.Domain.Models;

namespace TaskManagement.Infrastructure.Interfaces
{
    public interface IActivityLogRepository
    {
        Task AddLog(ActivityLog log); // Adds a new log
    }
}
