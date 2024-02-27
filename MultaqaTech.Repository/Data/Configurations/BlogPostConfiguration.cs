using Microsoft.EntityFrameworkCore;

namespace MultaqaTech.Repository.Data.Configurations;

internal class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.ToTable("BlogPosts");

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.PublishingDate)
            .IsRequired();

        //builder.HasOne(e => e.Author)
        //    .WithMany()
        //    .HasForeignKey(e => e.AuthorId);

        builder.HasOne(e => e.Category)
         .WithMany()
         .HasForeignKey(e => e.CategoryId)
         .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Comments)
        .WithOne(c => c.BlogPost)
        .HasForeignKey(c => c.BlogPostId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(bp => bp.Tags)
     .WithMany(t => t.BlogPosts);

    }
}
