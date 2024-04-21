namespace MultaqaTech.Service.BlogPostEntitiesServices;

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

        try
        {
            var blogPost = await _unitOfWork.Repository<BlogPost>().GetByIdWithSpecAsync(spec);

            if (blogPost is null) return null;

            return blogPost;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<IReadOnlyList<BlogPost>> ReadBlogPostsAsync(BlogPostSpeceficationsParams speceficationsParams)
    {
        var spec = new BlogPostWithIncludesSpecifications(speceficationsParams);

        try
        {
            var blogPosts = await _unitOfWork.Repository<BlogPost>().GetAllWithSpecAsync(spec);

            if (blogPosts is null) return null;

            return blogPosts;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<BlogPost?> UpdateBlogPost(int blogPostId, BlogPost updatedBlogPost)
    {
        var blogPost = await _unitOfWork.Repository<BlogPost>().GetByIdAsync(blogPostId);

        if (blogPost == null) return null;

        if (updatedBlogPost == null || string.IsNullOrWhiteSpace(updatedBlogPost.Title))
            return null;

        blogPost= updatedBlogPost;

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

    public async Task<bool> DeleteBlogPost(BlogPost blogPost)
    {
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
        var countSpec = new BlogPostWithFilterationForCountSpecifications(speceficationsParams);

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