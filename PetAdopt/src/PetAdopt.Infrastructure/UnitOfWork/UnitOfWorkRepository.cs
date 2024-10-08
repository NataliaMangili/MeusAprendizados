using Microsoft.EntityFrameworkCore;

namespace PetAdopt.Infrastructure.UnitOfWork;

public class UnitOfWorkRepository : IUnitOfWorkRepository
{
    private readonly PetContext _dbContext;
    private bool _disposed;

    public UnitOfWorkRepository(PetContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<bool> CommitAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public void BeginTransaction()
    {
        _disposed = false;
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
