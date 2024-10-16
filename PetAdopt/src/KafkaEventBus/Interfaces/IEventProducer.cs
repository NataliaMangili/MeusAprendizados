namespace KafkaEventBus.Interfaces;

public interface IEventProducer
{
    // Produz uma mensagem para um tópico
    Task ProduceAsync<T>(string topic, T message) where T : class;
}
