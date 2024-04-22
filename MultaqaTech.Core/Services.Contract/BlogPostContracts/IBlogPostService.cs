namespace MultaqaTech.Core.Services.Contract.BlogPostContracts;

public interface IBlogPostService
{
    Task<BlogPost?> CreateBlogPostAsync(BlogPost blogPost);

    Task<IReadOnlyList<BlogPost>> ReadBlogPostsAsync(BlogPostSpeceficationsParams speceficationsParams);

    Task<BlogPost?> ReadByIdAsync(int blogPostId);

    Task<BlogPost?> UpdateBlogPost(BlogPost storedBlogPost, BlogPost newBlogPost);

    Task<bool> DeleteBlogPost(BlogPost blogPost);

    Task<int> GetCountAsync(BlogPostSpeceficationsParams speceficationsParams);

    public Task<int> IncrementViewCountAsync(int postId);


}