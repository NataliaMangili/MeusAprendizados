using CrossCutting.Logging;

namespace PetAdopt.Infrastructure.Logging;

public class LogService : ILogService
{
    private readonly ILogRepository _logRepository;

    public LogService(ILogRepository logRepository)
    {
        _logRepository = logRepository;
    }

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
}