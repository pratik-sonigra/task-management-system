using TaskStatus = TaskManagement.Domain.Models.TaskStatus;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskStatusService
    {
        Task<List<TaskStatus>> GetAllStatuses(); // Retrieves all task statuses
        Task<TaskStatus> GetStatusById(int statusId); // Retrieve a task status by ID
        Task<TaskStatus> GetStatusByName(string statusName); // Retrieve a task status by name
    }
}
