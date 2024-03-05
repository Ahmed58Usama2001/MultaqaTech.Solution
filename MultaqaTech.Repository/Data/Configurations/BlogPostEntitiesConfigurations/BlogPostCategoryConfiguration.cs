using MultaqaTech.Core.Entities.BlogPostDomainEntities;

namespace MultaqaTech.Repository.Data.Configurations.BlogPostEntitiesConfigurations;

internal class BlogPostCategoryConfiguration : IEntityTypeConfiguration<BlogPostCategory>
{
    public void Configure(EntityTypeBuilder<BlogPostCategory> builder)
    {
        builder.ToTable("BlogPostCategories");

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(255);

    }
}
