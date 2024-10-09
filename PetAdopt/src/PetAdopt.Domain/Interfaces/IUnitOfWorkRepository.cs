namespace PetAdopt.Domain.Interfaces;

public interface IUnitOfWorkRepository : IDisposable
{
    Task RollbackTransactionAsync();
    Task CommitTransactionAsync();
    Task BeginTransactionAsync();
    /// <summary>
    /// Salva as mudanças feitas no contexto do banco de dados de forma assíncrona.
    /// </summary>
    /// <returns>O número de registros afetados.</returns>
    Task<int> CommitAsync();
    void BeginTransaction();
}
