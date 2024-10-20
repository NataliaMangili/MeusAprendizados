using CrossCutting.Logging;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace PetAdopt.Infrastructure.MongoLog;

public class LogRepository : ILogRepository
{
    private readonly IMongoCollection<LogDTO> _logCollection;
    //_context.Logs

    public LogRepository(IMongoDatabase database) => _logCollection = database.GetCollection<LogDTO>("Logs");

    public async Task LogAsync(LogDTO logEntry)
    {
        try
        {
            await _logCollection.InsertOneAsync(logEntry);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<IEnumerable<LogDTO>> GetAllLogsAsync(Expression<Func<LogDTO, bool>> filter = null)
    {
        // Se nenhum filtro for passado, retorna todos os logs
        var filterDefinition = filter != null ? Builders<LogDTO>.Filter.Where(filter) : Builders<LogDTO>.Filter.Empty;
        return await _logCollection.Find(filterDefinition).ToListAsync();
    }
}