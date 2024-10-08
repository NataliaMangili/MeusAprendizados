namespace PetAdopt.Domain.Interfaces;

public interface IUnitOfWorkRepository
{
    Task<bool> CommitAsync();
    void BeginTransaction();
    void SaveChanges();
}
