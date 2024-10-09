namespace KafkaEventBus.Interfaces;

public interface IEventConsumer
{
    // Consume mensagens de um tópico e processa usando um handler do MediaR
    void Consume<T>(string topic, Func<T, Task> handler) where T : class;
}