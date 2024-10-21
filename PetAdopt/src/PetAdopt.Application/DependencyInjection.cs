namespace PetAdopt.Application;
public static class DependencyInjection
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var application = typeof(IAssemblyMarker);

        services.AddMediatR(configure =>
        {
            configure.RegisterServicesFromAssembly(application.Assembly);
        });


        return services;
    }
}