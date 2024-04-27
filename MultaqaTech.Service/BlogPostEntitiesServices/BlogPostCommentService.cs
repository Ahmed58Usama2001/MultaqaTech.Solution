namespace MultaqaTech.Service.BlogPostEntitiesServices;

public class BlogPostCommentService(IUnitOfWork unitOfWork) : IBlogPostCommentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BlogPostComment?> CreateBlogPostAsync(BlogPostComment blogPostComment)
    {
        try
        {
            await _unitOfWork.Repository<BlogPostComment>().AddAsync(blogPostComment);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return blogPostComment;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteBlogPostComment(BlogPostComment blogPostComment)
    {
        try
        {
            _unitOfWork.Repository<BlogPostComment>().Delete(blogPostComment);

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

    public async Task<int> GetCountAsync(BlogPostCommentSpeceficationsParams speceficationsParams)
    {
        var countSpec = new BlogPostCommentsWithFilterationForCountSpecifications(speceficationsParams);

        var count = await _unitOfWork.Repository<BlogPostComment>().GetCountAsync(countSpec);

        return count;
    }

    public async Task<IReadOnlyList<BlogPostComment>> ReadAllBlogPostCommentsAsync(BlogPostCommentSpeceficationsParams speceficationsParams)
    {
        var spec = new BlogPostCommentWithIncludesSpecifications(speceficationsParams);

        var blogPostComments = await _unitOfWork.Repository<BlogPostComment>().GetAllWithSpecAsync(spec);

        return blogPostComments;
    }

    public async Task<BlogPostComment?> ReadByIdAsync(int blogPostCommentId)
    {
        var spec = new BlogPostCommentWithIncludesSpecifications(blogPostCommentId);

        var blogPostComment = await _unitOfWork.Repository<BlogPostComment>().GetByIdWithSpecAsync(spec);

        return blogPostComment;
    }

    public async Task<BlogPostComment?> UpdateBlogPostComment(int blogPostCommentId, BlogPostComment updatedBlogPostComment)
    {
        var blogPostComment = await _unitOfWork.Repository<BlogPostComment>().GetByIdAsync(blogPostCommentId);
        if (blogPostComment == null) return null;

        if (updatedBlogPostComment == null || string.IsNullOrWhiteSpace(updatedBlogPostComment.CommentContent))
            return null;

        blogPostComment = updatedBlogPostComment;

        try
        {
            _unitOfWork.Repository<BlogPostComment>().Update(blogPostComment);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return blogPostComment;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }
}