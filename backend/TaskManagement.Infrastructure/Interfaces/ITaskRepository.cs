using TaskManagement.Contracts.DTOs;
using TaskManagement.Domain.Models;

namespace TaskManagement.Infrastructure.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskEntity> GetTaskById(int taskId);
        Task<List<TaskEntity>> GetTasksByUserId(int userId);
        Task<IEnumerable<TaskResponse>> GetAllTasks(TaskFilterDto filter); // For admin
        Task AddTask(TaskEntity task);
        Task UpdateTask(TaskEntity task);
        Task DeleteTask(int taskId);
        Task<DashboardSummary> GetDashboardSummaryAsync();
    }
}
