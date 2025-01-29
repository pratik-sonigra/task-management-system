using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Infrastructure.Interfaces;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagement.Shared.Extensions
{
    public static class RepositoryLayerExtensions
    {
        public static IServiceCollection AddRepositoryLayer(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskStatusRepository, TaskStatusRepository>();
            services.AddScoped<IActivityLogRepository, ActivityLogRepository>();

            return services;
        }
    }
}
