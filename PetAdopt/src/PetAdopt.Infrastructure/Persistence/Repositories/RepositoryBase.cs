using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetAdopt.Infrastructure.Persistence.Repositories;

//public class RepositoryBase<TEntity>(PetContext dbContext, ILogger<RepositoryBase<TEntity>> logger) : IRepositoryBase<TEntity> where TEntity : class
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

    public async Task AddAsync<T>(T entity) where T : class
    {
        try
        {
            dbContext.Set<T>().Add(entity);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task UpdateAsync<T>(T entity) where T : class
    {
        try
        {
            dbContext.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;

            //return await DatabaseSaveChanges();
            //principle DRY
        }
        catch (Exception e)
        {
            throw;
        }
    }

    //Caso Repositorio Generico onde recebemos direto no repos, o T
    //public void UpdateAsync(TEntity entity)
    //{
    //    dbContext.Attach(entity);
    //    dbContext.Entry(entity).State = EntityState.Modified;
    //    dbContext.SaveChanges();
    //}

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
