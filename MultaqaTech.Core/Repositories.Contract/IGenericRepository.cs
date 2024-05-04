namespace MultaqaTech.Core.Repositories.Contract;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);

    Task<T?> GetByIdWithSpecAsync(ISpecifications<T> specs);

    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);

    Task<IReadOnlyList<T>> GetAllAsync();

    Task<IReadOnlyList<T>> GetAllByIdsAsync(List<int> ids);

    Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> specs);

    Task<int> GetCountAsync(ISpecifications<T> specs);

    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);
}