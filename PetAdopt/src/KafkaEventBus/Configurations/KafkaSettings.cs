namespace KafkaEventBus.Configurations;

public class KafkaSettings
{
    // Endereços dos servidores do Kafka
    public string BootstrapServers { get; set; }

    // Grupo de consumidores, coordenar o consumo de mensagens
    public string GroupId { get; set; }

    public string SecurityProtocol { get; set; }

    // Mecanismo de autenticação, ex: PLAIN
    public string SaslMechanism { get; set; }
    public string SaslUsername { get; set; }
    public string SaslPassword { get; set; }
}