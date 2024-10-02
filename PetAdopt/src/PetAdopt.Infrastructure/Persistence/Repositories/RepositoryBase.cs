using PetAdopt.Domain.Interfaces;
using PetAdopt.Infrastructure;

namespace PetAdopt.Infrastructure.Persistence.Repositories;

public class RepositoryBase(/*PetContext dbContext*/) : IRepositoryBase
{

    public async Task<bool> DatabaseSaveChanges()
    {
        try
        {
            return true;
            //return await dbContext.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            throw;
        }
    }

    //public async Task<bool> AddAsync<T>(T entity) where T : class
    //{
    //    try
    //    {
    //        var a = await dbContext.Set<T>().AddAsync(entity);
    //        var b = await dbContext.SaveChangesAsync() > 0;
    //        return b;
    //    }
    //    catch (Exception e)
    //    {
    //        throw;
    //    }
    //}

    //public async Task<S> GetAsync<T, S>(T id, S entity) where S : class
    //{
    //    var returned = (await dbContext.Set<S>().FindAsync(id))!;
    //    return returned;
    //}
}
