namespace PetAdopt.Domain.Interfaces;

public interface IRepositoryBase
{
    Task<bool> DatabaseSaveChanges();

    //Task<bool> AddAsync<T>(T entity) where T : class;

    //Task<S> GetAsync<T, S>(T id, S entity) where S : class;
}