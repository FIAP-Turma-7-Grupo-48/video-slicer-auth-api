using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Driver;

namespace WebApi.Extensions;

internal static class InfrastructureExtension
{
    private static string ConnectionString;

    static InfrastructureExtension()
    {
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connection)
    {
        ConnectionString += connection;

        return services
            .AddSqlRepositories()
            .AddClients();
    }

    private static IServiceCollection AddSqlRepositories(this IServiceCollection services)
    {
        return services.AddScoped<IUserMongoRepository, UserMongoRepository>();
    }

    private static IServiceCollection AddClients(this IServiceCollection services)
    {
        return services.AddSingleton<IMongoClient>(s =>
            new MongoClient(ConnectionString));
    }
}