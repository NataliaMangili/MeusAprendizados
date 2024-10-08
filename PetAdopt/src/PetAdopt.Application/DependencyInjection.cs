using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;

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