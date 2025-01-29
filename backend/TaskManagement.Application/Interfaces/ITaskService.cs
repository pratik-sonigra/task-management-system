using TaskManagement.Contracts.DTOs;
using TaskManagement.Domain.Models;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskEntity> GetTaskById(int taskId); // Retrieves a task by ID
        Task<List<TaskEntity>> GetTasksByUserId(int userId); // Fetches tasks assigned to a user
        Task<IEnumerable<TaskResponse>> GetAllTasks(TaskFilterDto filter); // Retrieves all tasks (admin only)
        Task<bool> AddTask(TaskEntity task); // Adds a new task
        Task<bool> UpdateTask(TaskEntity task); // Updates task details
        Task<bool> DeleteTask(int taskId); // Deletes a task
        Task<DashboardSummary> GetDashboardSummaryAsync();
    }
}
