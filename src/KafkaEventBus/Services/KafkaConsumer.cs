namespace KafkaEventBus.Services;
public class KafkaConsumer : IEventConsumer
{
    private readonly IConsumer<Null, string> _consumer;

    public KafkaConsumer(KafkaSettings settings)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = settings.BootstrapServers,
            GroupId = settings.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest, // Define o comportamento inicial do offset
            EnableAutoCommit = false // Desativa o commit automático para controlar manualmente
        };

        // Criando um consumidor Kafka com as configs
        _consumer = new ConsumerBuilder<Null, string>(config).Build();
    }

    // Consome mensagens de um tópico e processa com um handler (INotification)
    public void Consume<T>(string topic, Func<T, Task> handler) where T : class
    {
        // Inscreve o consumidor no tópico
        _consumer.Subscribe(topic);

        // Executa a leitura das mensagens em um loop assíncrono
        Task.Run(async () =>
        {
            while (true)
            {
                try
                {
                    // Consume uma mensagem do tópico
                    var consumeResult = _consumer.Consume();
                    Console.WriteLine($"Mensagem recebida: {consumeResult.Message.Value}");

                    var message = JsonConvert.DeserializeObject<T>(consumeResult.Message.Value);

                    // Processa a mensagem usando o handler escolhido
                    await handler(message);

                    // Confirma o consumo da mensagem
                    _consumer.Commit(consumeResult);
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occurred: {e.Error.Reason}");
                }
                finally
                {
                    _consumer.Close(); // Fecha o consumidor corretamente
                }
            }
        });
    }
}