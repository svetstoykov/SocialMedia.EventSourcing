using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Post.Command.Infrastructure.Settings;

namespace Post.Command.Infrastructure.Extensions;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbConfig>(configuration.GetSection(nameof(MongoDbConfig)));
        
        services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            // Retrieve the MongoDB connection settings from configuration
            var config = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString("MongoDB");

            // Create and configure a new MongoClient instance
            var mongoClient = new MongoClient(connectionString);

            return mongoClient;
        });

        services.Scan(scan => scan
            .FromCallingAssembly()
            .AddClasses()
            .AsMatchingInterface());

        return services;
    }
}