namespace MultaqaTech.Repository.Data.Configurations;

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

        //builder.HasOne(e => e.Author)
        //    .WithMany()
        //    .HasForeignKey(e => e.AuthorId);



    }
}
