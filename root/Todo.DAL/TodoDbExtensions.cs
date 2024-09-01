using Amazon.DynamoDBv2;
using Microsoft.Extensions.DependencyInjection;

namespace Todo.DAL
{
    public static class TodoDbExtensions
    {
        public static IServiceCollection RegisterDbDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();
            return services;
        }
    }
}
