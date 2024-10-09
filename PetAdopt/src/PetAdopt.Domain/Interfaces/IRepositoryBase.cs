namespace PetAdopt.Domain.Interfaces;

public interface IRepositoryBase/*<TEntity> where TEntity : class*/
{
    Task<bool> DatabaseSaveChanges();

    Task AddAsync<T>(T entity) where T : class;

    Task UpdateAsync<T>(T entity) where T : class;

    Task<T?> GetAsync<T>(object id) where T : class;
}