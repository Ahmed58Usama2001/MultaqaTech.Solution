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
            .WithOne(b => b.Category)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
