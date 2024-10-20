using CrossCutting.Logging;

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PetAdopt.Infrastructure.Logging;

namespace PetAdopt.Infrastructure.MongoLog;

public static class MongoDBExtensions
{
    public static IServiceCollection ConfigureMongo(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoSettings = new MongoDBSettings
        {
            ConnectionString = configuration.GetSection("MongoDB").GetSection("ConnectionString").Value
            ?? throw new ArgumentNullException("there's no ConnectionString for MongoDB"),

            DatabaseName = configuration.GetSection("MongoDB").GetSection("DatabaseName").Value 
            ?? throw new ArgumentNullException("there's no DatabaseName for MongoDB"),
        };

        services.AddSingleton(mongoSettings);

        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<MongoDBSettings>();
            return new MongoClient(settings.ConnectionString);
        });

        // AddScoped: Uma nova instância é criada para cada solicitação
        services.AddScoped<IMongoDatabase>(sp =>
        {
            var settings = sp.GetRequiredService<MongoDBSettings>();

            // IMongoClient que foi registrada aqui
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        });
       
        services.AddScoped<ILogRepository, LogRepository>();
        services.AddScoped<ILogService, LogService>();

        return services;
    }
}