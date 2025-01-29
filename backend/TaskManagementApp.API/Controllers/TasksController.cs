using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Interfaces;
using TaskManagement.Contracts.DTOs;
using TaskManagement.Domain.Models;

namespace TaskManagementApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ITaskStatusService _taskStatusService;

        public TasksController(ITaskService taskService, ITaskStatusService taskStatusService)
        {
            _taskService = taskService;
            _taskStatusService = taskStatusService;
        }

        [HttpPost("task-list")]
        [Authorize(Roles = "User,Admin")]    
        public async Task<IActionResult> GetAllTasks([FromBody] TaskFilterDto filter)
        {
            var tasks = await _taskService.GetAllTasks(filter);
            return Ok(tasks);
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetTasksByUserId(int userId)
        {
            var tasks = await _taskService.GetTasksByUserId(userId);
            var response = tasks.Select(t => new TaskResponse
            {
                TaskId = t.TaskId,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status.StatusName,
                AssignedUser = t.AssignedUser.Username,
                DueDate = t.DueDate,
                CreatedBy = t.CreatedByNavigation.Username,
                CreatedAt = t.CreatedAt.HasValue ? t.CreatedAt.Value : DateTime.Now
            });
            return Ok(response);
        }

        [HttpGet("{taskId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            var task = await _taskService.GetTaskById(taskId);
            if (task == null)
            {
                return NotFound(new { message = $"Task with ID {taskId} not found." });
            }
            var response = new TaskResponse
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.StatusName,
                StatusId = task.StatusId,
                AssignedUser = task.AssignedUser.Username,
                AssignedUserId = task.AssignedUserId.Value,
                DueDate = task.DueDate,
                CreatedBy = task.CreatedByNavigation.Username,
                CreatedAt = task.CreatedAt.HasValue ? task.CreatedAt.Value : DateTime.Now
            };
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTask([FromBody] TaskRequest request)
        {
            var task = new TaskEntity
            {
                Title = request.Title,
                Description = request.Description,
                StatusId = request.StatusId,
                AssignedUserId = request.AssignedUserId,
                DueDate = request.DueDate,
                CreatedBy = request.CreatedBy
            };

            var success = await _taskService.AddTask(task);
            if (!success)
            {
                return BadRequest(new { message = "Failed to create task." });
            }
            return Ok(new { message = "Task created successfully." });
        }

        [HttpPut("{taskId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTask(int taskId, [FromBody] TaskRequest request)
        {
            var task = new TaskEntity
            {
                TaskId = taskId,
                Title = request.Title,
                Description = request.Description,
                StatusId = request.StatusId,
                AssignedUserId = request.AssignedUserId,
                DueDate = request.DueDate,
                CreatedBy = request.CreatedBy
            };

            var success = await _taskService.UpdateTask(task);
            if (!success)
            {
                return BadRequest(new { message = "Failed to update task." });
            }
            return Ok(new { message = "Task updated successfully." });
        }

        [HttpDelete("{taskId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            var success = await _taskService.DeleteTask(taskId);
            if (!success)
            {
                return BadRequest(new { message = "Failed to delete task." });
            }
            return Ok(new { message = "Task deleted successfully." });
        }

        [HttpGet("status")]
        [Authorize]
        public async Task<IActionResult> GetTaskStatuses()
        {
            var statuses = await _taskStatusService.GetAllStatuses();

            return Ok(statuses);
        }

        [HttpGet("dashboard-summary")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            var summary = await _taskService.GetDashboardSummaryAsync();
            return Ok(summary);
        }
    }
}
