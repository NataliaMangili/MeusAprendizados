using CrossCutting.Logging;
using MongoDB.Bson;

namespace PetAdopt.Infrastructure.Logging;

public class LogService : ILogService
{
    private readonly ILogRepository _logRepository;

    public LogService(ILogRepository logRepository) => _logRepository = logRepository;

    public async Task LogInformation(string message)
    {
        var logEntry = new LogDTO { Message = message, Level = "Information" };
        await _logRepository.LogAsync(logEntry);
    }

    public async Task LogWarning(string message)
    {
        var logEntry = new LogDTO { Message = message, Level = "Warning" };
        await _logRepository.LogAsync(logEntry);
    }

    public async Task LogError(string message, Exception ex)
    {
        var logEntry = new LogDTO { Message = message, Level = "Error", Exception = ex.ToString() };
        await _logRepository.LogAsync(logEntry);
    }

    //Pode ser um BsonDocument também
    public async Task<IEnumerable<LogDTO>> GetErrorLogsAsync()
    {
        var errorLogs = await _logRepository.GetAllLogsAsync(log => log.Level == "Error");
        return errorLogs;

        //var keyword = "falha";
        //var logsKeyword = await GetAllLogsAsync(log => log.Message.Contains(keyword));
    }
}