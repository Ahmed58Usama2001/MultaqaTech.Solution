
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
    }
}
