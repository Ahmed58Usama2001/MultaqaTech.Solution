namespace MultaqaTech.Repository.Data.Configurations;

public class MultaqaTechContext : DbContext
{

    public MultaqaTechContext(DbContextOptions<MultaqaTechContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<BlogPostCategory> BlogPostCategories { get; set; }
    public DbSet<BlogPostComment> BlogPostComments { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }
}
