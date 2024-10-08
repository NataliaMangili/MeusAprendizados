using Microsoft.Extensions.Logging;

namespace PetAdopt.Infrastructure.Persistence.Repositories;

public class RepositoryBase(PetContext dbContext, ILogger<RepositoryBase> logger) : IRepositoryBase
{

    public async Task<bool> DatabaseSaveChanges()
    {
        try
        {
            var success = await dbContext.SaveChangesAsync() > 0;
            return success;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error saving changes to the database");
            throw; 
        }
    }

    public async Task<bool> AddAsync<T>(T entity) where T : class
    {
        try
        {
            //return _context.Ngo.Add(ngo).Entity;

            dbContext.Set<T>().Add(entity);
            return true;
            //return await DatabaseSaveChanges();
            //principle DRY
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<T?> GetAsync<T>(object id) where T : class
    {
        try
        {
            var entity = await dbContext.Set<T>().FindAsync(id);

            if (entity == null)
            {
                logger.LogWarning("Entity of type {EntityType} with ID {Id} was not found.", typeof(T).Name, id);
            }

            return entity;
        }
        catch (Exception e)
        {
            // Loga o tipo da entidade e o ID que causou o erro
            logger.LogError(e, "Error fetching entity of type {EntityType} with ID {Id}.", typeof(T).Name, id);
            throw;
        }
    }
}
