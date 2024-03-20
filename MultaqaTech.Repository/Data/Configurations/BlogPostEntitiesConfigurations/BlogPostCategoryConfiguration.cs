
namespace MultaqaTech.Repository.Data.Configurations.BlogPostEntitiesConfigurations;

internal class BlogPostCategoryConfiguration : IEntityTypeConfiguration<BlogPostCategory>
{
    const int maxLength = 255;

    public void Configure(EntityTypeBuilder<BlogPostCategory> builder)
    {
        builder.ToTable("BlogPostCategories");

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(maxLength);

        builder.HasMany(bp => bp.BlogPosts)
              .WithOne(c => c.Category)
              .HasForeignKey(c => c.BlogPostCategoryId)
              .OnDelete(DeleteBehavior.Restrict);

    }
}
