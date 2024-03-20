
namespace MultaqaTech.Repository.Data.Configurations.BlogPostEntitiesConfigurations;

internal class BlogPostCommentConfiguration : IEntityTypeConfiguration<BlogPostComment>
{
    public void Configure(EntityTypeBuilder<BlogPostComment> builder)
    {
        builder.ToTable("BlogPostComments");

        builder.Property(e => e.CommentContent)
            .IsRequired();

        builder.Property(e => e.DatePosted)
            .IsRequired();

        builder.Property(e => e.BlogPostId)
            .IsRequired();

        builder.HasOne(bp => bp.BlogPost)
                 .WithMany(c => c.Comments)
                 .HasForeignKey(bp => bp.BlogPostId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.NoAction);
    }
}
