using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace CrossCutting.Logging;
public interface ILogRepository
{
    Task LogAsync(LogDTO logEntry);
    Task<IEnumerable<LogDTO>> GetAllLogsAsync(Expression<Func<LogDTO, bool>> filter = null);
}
