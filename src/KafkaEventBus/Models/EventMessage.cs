namespace KafkaEventBus.Models;

public class EventMessage
{
    // Id único da mensagem
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Timestamp { get; set; } = DateTime.UtcNow.ToString("o");
    public string Payload { get; set; }
}