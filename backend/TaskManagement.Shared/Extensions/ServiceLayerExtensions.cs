using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;

namespace TaskManagement.Shared.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskStatusService, TaskStatusService>();
            services.AddScoped<IActivityLogService, ActivityLogService>();

            return services;
        }
    }
}
