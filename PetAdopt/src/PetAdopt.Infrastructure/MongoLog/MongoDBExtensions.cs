using CrossCutting.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PetAdopt.Domain.Interfaces;
using PetAdopt.Infrastructure.Logging;
using PetAdopt.Infrastructure.Persistence.Repositories;
using System.Configuration;

namespace PetAdopt.Infrastructure.MongoLog;

public static class MongoDBExtensions
{
    public static IServiceCollection ConfigureMongo(this IServiceCollection services, IConfiguration configuration)
    {
       //services.Configure<MongoDBSettings>(configuration.GetSection("MongoDB"));

        configuration.GetSection("MongoDB");

        services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        services.AddScoped<IMongoDatabase>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        });

        //DI
        services.AddScoped<ILogRepository, LogRepository>();
        services.AddScoped<ILogService, LogService>();

        return services;
    }
}