using Microsoft.EntityFrameworkCore.Storage;

namespace PetAdopt.Infrastructure.UnitOfWork;

public class UnitOfWorkRepository(PetContext dbContext) : IUnitOfWorkRepository
{
    private readonly PetContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private IDbContextTransaction _currentTransaction;
    private bool _disposed; // Indica se a unidade de trabalho foi descartada.

    public async Task BeginTransactionAsync()
    {
        // Se já existe uma transação ativa, retorna
        if (_currentTransaction != null)
        {
            return;
        }

        // Inicia uma nova transação e a guarda na variável _currentTransaction
        _currentTransaction = await _dbContext.Database.BeginTransactionAsync();
    }

    // Confirmar a transação atual
    public async Task CommitTransactionAsync()
    {
        try
        {
            // Salva as mudanças feitas no contexto do banco de dados e confirma a transação, aplicando as mudanças
            await _dbContext.SaveChangesAsync();
            await _currentTransaction.CommitAsync();
        }
        catch
        {
            await _currentTransaction.RollbackAsync();
            // Relança a exceção para que possa ser tratada em outro lugar.
            throw;
        }
        finally
        {
            await DisposeTransactionAsync(); // Descartar a transação
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.RollbackAsync(); // Rollback
        }
        await DisposeTransactionAsync(); // Descartar a transação
    }

    private async Task DisposeTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.DisposeAsync(); // Libera a transação
            _currentTransaction = null; // Limpa a referência
        }
    }

    public async Task<int> CommitAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void BeginTransaction()
    {
        _disposed = false;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose(); // Libera o contexto
            }
            _disposed = true; // Marca como descartado
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}