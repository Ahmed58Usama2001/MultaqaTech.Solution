namespace MultaqaTech.Core.Services.Contract.BlogPostContracts;

public interface IBlogPostCommentService
{
    Task<BlogPostComment?> CreateBlogPostAsync(BlogPostComment blogPostComment);

    Task<IReadOnlyList<BlogPostComment>> ReadBlogPostCommentsAsync(BlogPostCommentSpeceficationsParams speceficationsParams);

    Task<BlogPostComment?> ReadByIdAsync(int blogPostCommentId);

    Task<BlogPostComment?> UpdateBlogPostComment(int blogPostCommentId, BlogPostComment updatedBlogPostComment);

    Task<bool> DeleteBlogPostComment(BlogPostComment blogPostComment);

    Task<int> GetCountAsync(BlogPostCommentSpeceficationsParams speceficationsParams);
}