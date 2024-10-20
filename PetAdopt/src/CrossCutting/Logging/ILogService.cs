namespace CrossCutting.Logging;
//SOLID, abstrações
public interface ILogService
{
    Task LogInformation(string message);
    Task LogWarning(string message);
    Task LogError(string message, Exception ex);
    Task<IEnumerable<LogDTO>> GetErrorLogsAsync();
}

