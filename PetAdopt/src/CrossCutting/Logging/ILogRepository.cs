using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CrossCutting.Logging;
public interface ILogRepository
{
    Task LogAsync(LogDTO logEntry);
}
