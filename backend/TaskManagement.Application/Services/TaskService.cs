using TaskManagement.Application.Interfaces;
using TaskManagement.Contracts.DTOs;
using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.Interfaces;

namespace TaskManagement.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskEntity> GetTaskById(int taskId)
        {
            return await _taskRepository.GetTaskById(taskId);
        }

        public async Task<IEnumerable<TaskResponse>> GetAllTasks(TaskFilterDto filter)
        {
            return await _taskRepository.GetAllTasks(filter);
        }

        public async Task<List<TaskEntity>> GetTasksByUserId(int userId)
        {
            return await _taskRepository.GetTasksByUserId(userId);
        }

        public async Task<bool> AddTask(TaskEntity task)
        {
            try
            {
                await _taskRepository.AddTask(task);
                return true;
            }
            catch
            {
                // Handle exceptions or logging as needed
                return false;
            }
        }

        public async Task<bool> UpdateTask(TaskEntity task)
        {
            try
            {
                await _taskRepository.UpdateTask(task);
                return true;
            }
            catch
            {
                // Handle exceptions or logging as needed
                return false;
            }
        }

        public async Task<bool> DeleteTask(int taskId)
        {
            try
            {
                await _taskRepository.DeleteTask(taskId);
                return true;
            }
            catch
            {
                // Handle exceptions or logging as needed
                return false;
            }
        }

        public async Task<DashboardSummary> GetDashboardSummaryAsync()
        {
            return await _taskRepository.GetDashboardSummaryAsync();
        }
    }
}
