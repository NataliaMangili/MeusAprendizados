namespace CrossCutting.Logging;

public class LogDTO
{
    public Guid Id { get; set; }
    public string Level { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
    public string Exception { get; set; }
}
