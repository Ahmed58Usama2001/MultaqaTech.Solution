namespace MultaqaTech.Core.Services.Contract;

public interface IBlogPostCategoryService
{
    Task<BlogPostCategory?> CreateBlogPostCategoryAsync(BlogPostCategory category);

    Task<IReadOnlyList<BlogPostCategory>> ReadAllAsync();

    Task<BlogPostCategory?> ReadByIdAsync(int categoryId);

    Task<BlogPostCategory?> UpdateBlogPostCategory(int categoryId, BlogPostCategory category);

    Task<bool> DeleteBlogPostCategory(int categoryId);
}