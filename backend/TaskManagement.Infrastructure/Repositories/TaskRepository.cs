using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TaskManagement.Contracts.DTOs;
using TaskManagement.Domain.Models;
using TaskManagement.Infrastructure.Interfaces;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementContext _context;

        public TaskRepository(TaskManagementContext context)
        {
            _context = context;
        }

        public async Task<TaskEntity> GetTaskById(int taskId)
        {
            return await _context.Tasks
                .Include(t => t.Status)
                .Include(t => t.AssignedUser)
                .Include(t => t.CreatedByNavigation)
                .FirstOrDefaultAsync(t => t.TaskId == taskId);
        }

        public async Task<IEnumerable<TaskResponse>> GetAllTasks(TaskFilterDto filter)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Title", filter.Title);
            parameters.Add("@StatusId", filter.StatusId);
            parameters.Add("@DueDate", filter.DueDate);
            parameters.Add("@UserId", filter.UserId);

            var tasks = await _context.Database.GetDbConnection().QueryAsync<TaskResponse>(
                "dbo.usp_GetTasks",
                parameters,
                commandType: CommandType.StoredProcedure);

            return tasks;
        }

        public async Task<List<TaskEntity>> GetTasksByUserId(int userId)
        {
            return await _context.Tasks
                .Include(t => t.Status)
                .Include(t => t.AssignedUser)
                .Include(t => t.CreatedByNavigation)
                .Where(t => t.AssignedUserId == userId)
                .ToListAsync();
        }

        public async Task AddTask(TaskEntity task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTask(TaskEntity task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTask(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<DashboardSummary> GetDashboardSummaryAsync()
        {
            var dashboardSummary = new DashboardSummary
            {
                TasksByStatus = new List<TaskStatusCount>(),
                TasksPerUser = new List<UserTaskCount>()
            };

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "EXEC dbo.usp_GetDashboardSummary";
                command.CommandType = CommandType.Text;

                await _context.Database.OpenConnectionAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    // First result set: TotalTasks
                    if (await reader.ReadAsync())
                    {
                        dashboardSummary.TotalTasks = reader.GetInt32(0);
                    }

                    // Move to the second result set: TasksByStatus
                    if (await reader.NextResultAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            dashboardSummary.TasksByStatus.Add(new TaskStatusCount
                            {
                                Status = reader.GetString(0),
                                Count = reader.GetInt32(1)
                            });
                        }
                    }

                    // Move to the third result set: TasksPerUser
                    if (await reader.NextResultAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            dashboardSummary.TasksPerUser.Add(new UserTaskCount
                            {
                                UserName = reader.GetString(0),
                                TaskCount = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }

            return dashboardSummary;
        }
    }
}
