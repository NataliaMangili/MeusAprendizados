using PetAdopt.Infrastructure.UnitOfWork;

namespace PetAdopt.Infrastructure.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        // Registrar a UnitOfWork
        services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();
        services.AddTransient<IRepositoryBase, RepositoryBase>();
        services.AddTransient<INgoRepository, NgoRepository>();

        return services;
    }
}
