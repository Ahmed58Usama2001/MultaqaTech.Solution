namespace MultaqaTech.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly MultaqaTechContext _multaqaTechContext;

    private readonly Hashtable _repositories;

    public UnitOfWork(MultaqaTechContext multaqaTechContext) //Ask CLR To create object from DB Context Implicitly
    {
        _multaqaTechContext = multaqaTechContext;
        _repositories = [];
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity  //This Method to create repository per request
    {
        var key = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(key))
        {
            var repository = new GenericRepository<TEntity>(_multaqaTechContext);
            _repositories.Add(key, repository);
        }

        return _repositories[key] as IGenericRepository<TEntity>;
    }
    public async Task<int> CompleteAsync()
        => await _multaqaTechContext.SaveChangesAsync();

    public async ValueTask DisposeAsync()
    => await _multaqaTechContext.DisposeAsync();
}