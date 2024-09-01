using Microsoft.Extensions.DependencyInjection;
using Todo.DAL.Repositories;
using Todo.DAL.Repositories.Interfaces;

namespace Todo.DAL
{
    public static class TodoRepositoryExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IBaseRepository, BaseRepository>();
            return services;
        }
    }
}
