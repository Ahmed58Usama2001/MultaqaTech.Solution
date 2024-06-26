﻿
namespace MultaqaTech.Repository.Data.Configurations.BlogPostEntitiesConfigurations;

internal class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    const int shortMaxLength = 255;
    const int longMaxLength = 450;

    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.ToTable("BlogPosts");

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(longMaxLength);

        builder.Property(e => e.Content)
        .IsRequired();

        builder.Property(e => e.PublishingDate)
        .IsRequired();

        builder.Property(e => e.BlogPostCategoryId)
         .IsRequired();

        builder.Ignore(l => l.MediaUrl);


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
