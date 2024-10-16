namespace CrossCutting.Logging;
public interface ILogService
{
    Task LogInformation(string message);
    Task LogWarning(string message);
    Task LogError(string message, Exception ex);
}

