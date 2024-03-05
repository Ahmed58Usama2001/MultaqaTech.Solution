
using MultaqaTech.Core.Entities.BlogPostDomainEntities;

namespace MultaqaTech.Service;

public class BlogPostService(IUnitOfWork unitOfWork) : IBlogPostService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BlogPost?> CreateBlogPostAsync(BlogPost blogPost)
    {
        try
        {
            await _unitOfWork.Repository<BlogPost>().AddAsync(blogPost);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return blogPost;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<BlogPost?> ReadByIdAsync(int blogPostId)
    {
        var spec = new BlogPostWithIncludesSpecifications(blogPostId);

        var blogPost = await _unitOfWork.Repository<BlogPost>().GetByIdWithSpecAsync(spec);

        return blogPost;
    }

    public async Task<IReadOnlyList<BlogPost>> ReadBlogPostsAsync(BlogPostSpeceficationsParams speceficationsParams)
    {
        var spec = new BlogPostWithIncludesSpecifications(speceficationsParams);

        var blogPosts = await _unitOfWork.Repository<BlogPost>().GetAllWithSpecAsync(spec);

        return blogPosts;
    }

    public async Task<BlogPost?> UpdateBlogPost(int blogPostId, BlogPost updatedBlogPost)
    {
        var blogPost = await _unitOfWork.Repository<BlogPost>().GetByIdAsync(blogPostId);

        if (blogPost == null) return null;

        if (updatedBlogPost == null || string.IsNullOrWhiteSpace(updatedBlogPost.Title))
            return null;

        blogPost.Title = updatedBlogPost.Title;
        blogPost.Content = updatedBlogPost.Content;
        blogPost.NumberOfViews = updatedBlogPost.NumberOfViews;
        blogPost.Category = updatedBlogPost.Category;
        blogPost.BlogPostCategoryId = updatedBlogPost.BlogPostCategoryId;
        blogPost.PublishingDate = updatedBlogPost.PublishingDate;
        blogPost.Tags = updatedBlogPost.Tags;
        blogPost.Comments = updatedBlogPost.Comments;

        try
        {
            _unitOfWork.Repository<BlogPost>().Update(blogPost);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return blogPost;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteBlogPost(int blogPostId)
    {
        var blogPost = await _unitOfWork.Repository<BlogPost>().GetByIdAsync(blogPostId);

        if (blogPost == null)
            return false;

        try
        {
            _unitOfWork.Repository<BlogPost>().Delete(blogPost);

            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                return false;

            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return false;
        }
    }
 

    public async Task<int> GetCountAsync(BlogPostSpeceficationsParams speceficationsParams)
    {
        var countSpec = new BlogPostsWithFilterationForCountSpecifications(speceficationsParams);

        var count = await _unitOfWork.Repository<BlogPost>().GetCountAsync(countSpec);

        return count;
    }

    public async Task<int> IncrementViewCountAsync(int postId)
    {
        var blogPost = await _unitOfWork.Repository<BlogPost>().GetByIdAsync(postId);

        if (blogPost != null)
        {
            blogPost.NumberOfViews++;
            await _unitOfWork.CompleteAsync();

            return blogPost.NumberOfViews;
        }
        else
        {
            throw new ArgumentException($"Blog post with ID {postId} not found.");
        }
    }
}