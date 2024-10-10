namespace KafkaEventBus.Services;

public class KafkaProducer : IEventProducer
{
    private readonly IProducer<Null, string> _producer;

    public KafkaProducer(KafkaSettings settings)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = settings.BootstrapServers,
            SecurityProtocol = SecurityProtocol.Plaintext
        };

        // Criando um produtor Kafka com as configs
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    // Produz mensagens para um tópico específico
    public async Task ProduceAsync<T>(string topic, T message) where T : class
    {
        try
        {
            var serializedMessage = JsonConvert.SerializeObject(message);

            // Envia a mensagem para o Kafka
            await _producer.ProduceAsync(topic, new Message<Null, string> { Value = serializedMessage });
            Console.WriteLine($"Mensagem publicada no tópico {topic}: {serializedMessage}");

        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }

    }
}
