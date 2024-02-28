namespace MultaqaTech.Repository.Data.Configurations;

internal class BlogPostCategoryConfiguration : IEntityTypeConfiguration<BlogPostCategory>
{
    public void Configure(EntityTypeBuilder<BlogPostCategory> builder)
    {
        builder.ToTable("BlogPostCategories");

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasMany(e => e.BlogPosts)
    .WithOne(c => c.Category)
    .HasForeignKey(c => c.CategoryId)
    .OnDelete(DeleteBehavior.Cascade);

    }
}
