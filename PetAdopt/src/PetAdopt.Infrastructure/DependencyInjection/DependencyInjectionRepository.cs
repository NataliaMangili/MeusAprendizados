namespace PetAdopt.Infrastructure.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        //services.AddDbContext<TMContext>(options =>
        //{
        //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        //});

        services.AddTransient<IRepositoryBase, RepositoryBase>();

        return services;
    }
}
