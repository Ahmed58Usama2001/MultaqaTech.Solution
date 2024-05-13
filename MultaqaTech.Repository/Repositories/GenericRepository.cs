namespace MultaqaTech.Repository.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly MultaqaTechContext _dbContext;

    public GenericRepository(MultaqaTechContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllByIdsAsync(List<int> ids)
    {
        return await _dbContext.Set<T>().Where(entity => ids.Contains(entity.Id)).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> specs)
    {
        return await ApplySpecifications(specs).ToListAsync();
    }

    public async Task<T?> GetByIdWithSpecAsync(ISpecifications<T> specs)
    {
        return await ApplySpecifications(specs).FirstOrDefaultAsync();
    }

    public async Task<int> GetCountAsync(ISpecifications<T> specs)
    {
        return await ApplySpecifications(specs).CountAsync();
    }

    private IQueryable<T> ApplySpecifications(ISpecifications<T> specs)
    {
        return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), specs);
    }

    public async Task AddAsync(T entity)
        => await _dbContext.AddAsync(entity);

    public async Task BulkAddAsync(List<T> entities)
        => await _dbContext.AddRangeAsync(entities);

    public void Update(T entity)
    => _dbContext.Update(entity);

    public void Delete(T entity)
    => _dbContext.Remove(entity);
}