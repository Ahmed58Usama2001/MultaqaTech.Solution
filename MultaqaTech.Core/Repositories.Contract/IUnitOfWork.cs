namespace MultaqaTech.Core.Repositories.Contract;

public interface IUnitOfWork : IAsyncDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

    Task<int> CompleteAsync();
}