namespace KafkaEventBus.Configurations;

public static class KafkaDependencyInjection
{
    public static IServiceCollection KafkaConfigurationDI(this IServiceCollection services, KafkaSettings settings)
    {
        services.AddSingleton(settings);
        services.AddSingleton<IEventProducer, KafkaProducer>();
        services.AddSingleton<IEventConsumer, KafkaConsumer>();

        return services;
    }
}