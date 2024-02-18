namespace MultaqaTech.Repository.Data;

public class MultaqaTechContext:DbContext
{

    public MultaqaTechContext(DbContextOptions<MultaqaTechContext>options)
        :base(options) 
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
