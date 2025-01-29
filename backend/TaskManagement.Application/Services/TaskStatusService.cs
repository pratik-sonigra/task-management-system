using TaskManagement.Application.Interfaces;
using TaskManagement.Infrastructure.Interfaces;
using TaskStatus = TaskManagement.Domain.Models.TaskStatus;

namespace TaskManagement.Application.Services
{
    public class TaskStatusService : ITaskStatusService
    {
        private readonly ITaskStatusRepository _taskStatusRepository;

        public TaskStatusService(ITaskStatusRepository taskStatusRepository)
        {
            _taskStatusRepository = taskStatusRepository;
        }

        public async Task<List<TaskStatus>> GetAllStatuses()
        {
            return await _taskStatusRepository.GetAllStatuses();
        }

        public async Task<TaskStatus> GetStatusById(int statusId)
        {
            return await _taskStatusRepository.GetStatusById(statusId);
        }

        public async Task<TaskStatus> GetStatusByName(string statusName)
        {
            return await _taskStatusRepository.GetStatusByName(statusName);
        }
    }
}
