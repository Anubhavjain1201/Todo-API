using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Todo.DAL
{
    public static class TodoDbExtensions
    {
        public static IServiceCollection RegisterDbDependencies (this IServiceCollection services, TodoDbOptions dbOptions)
        {
            services.AddDbContext<TodoDbContext>(options =>
            {
                options.UseInMemoryDatabase(dbOptions.DB_NAME);
            });
            return services;
        }
    }
}
