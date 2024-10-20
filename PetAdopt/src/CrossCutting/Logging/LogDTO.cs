using MongoDB.Bson.Serialization.Attributes;

namespace CrossCutting.Logging;

public class LogDTO
{
    //[BsonId]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Level { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Exception { get; set; }
}
