using TaskManagement.Domain.Models;

namespace TaskManagement.Application.Interfaces
{
    public interface IActivityLogService
    {
        Task AddLog(ActivityLog log); // Adds a new log entry
    }
}
