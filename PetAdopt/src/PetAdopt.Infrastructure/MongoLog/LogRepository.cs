using CrossCutting.Logging;
using MongoDB.Driver;

namespace PetAdopt.Infrastructure.MongoLog;

public class LogRepository : ILogRepository
{
    private readonly IMongoCollection<LogDTO> _logCollection;

    public LogRepository(IMongoDatabase database)
    {
        _logCollection = database.GetCollection<LogDTO>("Logs");
    }

    public async Task LogAsync(LogDTO logEntry)
    {
        await _logCollection.InsertOneAsync(logEntry);
    }
}