namespace MultaqaTech.Repository.Data.Configurations;

internal class BlogPostCommentConfiguration : IEntityTypeConfiguration<BlogPostComment>
{
    public void Configure(EntityTypeBuilder<BlogPostComment> builder)
    {
        builder.ToTable("BlogPostComments");

        builder.Property(e => e.Content)
            .IsRequired();

        builder.Property(e => e.DatePosted)
            .IsRequired();

        //builder.HasOne(e => e.Author)
        //    .WithMany()
        //    .HasForeignKey(e => e.AuthorId);

        builder.HasOne(e => e.BlogPost)
             .WithMany() 
             .HasForeignKey(e => e.BlogPostId)
             .OnDelete(DeleteBehavior.Cascade);


    }
}
