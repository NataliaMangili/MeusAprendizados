using Microsoft.Extensions.Logging;

namespace PetAdopt.Infrastructure.Persistence.Repositories;

public class NgoRepository(PetContext dbContext, ILogger<NgoRepository> logger) : INgoRepository
{
    public Ngo Add(Ngo ngo)
    {
        return dbContext.Ngos.Add(ngo).Entity;
    }
}
