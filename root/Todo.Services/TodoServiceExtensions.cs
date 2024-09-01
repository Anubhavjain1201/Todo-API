using Microsoft.Extensions.DependencyInjection;

namespace Todo.Services
{
    public static class TodoServiceExtensions
    {
        public static IServiceCollection RegisterTodoServices(this IServiceCollection services)
        {
            services.AddSingleton<ITodoService, TodoService>();
            return services;
        }
    }
}
