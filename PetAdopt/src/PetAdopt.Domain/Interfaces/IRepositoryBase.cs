namespace PetAdopt.Domain.Interfaces;

public interface IRepositoryBase
{
    Task<bool> DatabaseSaveChanges();

    Task<bool> AddAsync<T>(T entity) where T : class;

    Task<T?> GetAsync<T>(object id) where T : class;
}