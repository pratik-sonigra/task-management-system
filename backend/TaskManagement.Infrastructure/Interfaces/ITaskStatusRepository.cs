using TaskStatus = TaskManagement.Domain.Models.TaskStatus;

namespace TaskManagement.Infrastructure.Interfaces
{
    public interface ITaskStatusRepository
    {
        Task<List<TaskStatus>> GetAllStatuses();
        Task<TaskStatus> GetStatusById(int statusId); // Retrieve a task status by ID
        Task<TaskStatus> GetStatusByName(string statusName); // Retrieve a task status by name
    }
}
