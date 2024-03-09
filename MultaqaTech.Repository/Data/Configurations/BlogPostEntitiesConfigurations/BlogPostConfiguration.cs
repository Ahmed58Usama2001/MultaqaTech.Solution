
namespace MultaqaTech.Repository.Data.Configurations.BlogPostEntitiesConfigurations;

internal class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.ToTable("BlogPosts");

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.Content)
        .IsRequired();

        builder.Property(e => e.PublishingDate)
        .IsRequired();

        builder.Property(e => e.BlogPostCategoryId)
         .IsRequired();


        builder.HasOne(bp => bp.Category)
                 .WithMany(c => c.BlogPosts)
                 .HasForeignKey(bp => bp.BlogPostCategoryId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);


        builder.HasMany(bp => bp.Comments)
               .WithOne(c => c.BlogPost)
               .HasForeignKey(c => c.BlogPostId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(bp => bp.Tags)
          .WithMany(t => t.BlogPosts);
    }
}
