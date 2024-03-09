namespace MultaqaTech.Service.BlogPostEntitiesServices;

public class BlogPostCategoryService(IUnitOfWork unitOfWork) : IBlogPostCategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BlogPostCategory?> CreateBlogPostCategoryAsync(BlogPostCategory category)
    {
        try
        {
            await _unitOfWork.Repository<BlogPostCategory>().AddAsync(category);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return category;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteBlogPostCategory(int categoryId)
    {
        var category = await _unitOfWork.Repository<BlogPostCategory>().GetByIdAsync(categoryId);

        if (category == null)
            return false;

        try
        {
            _unitOfWork.Repository<BlogPostCategory>().Delete(category);

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

    public async Task<IReadOnlyList<BlogPostCategory>> ReadAllAsync()
    {
        try
        {
            var categories = await _unitOfWork.Repository<BlogPostCategory>().GetAllAsync();

            return categories;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<BlogPostCategory?> ReadByIdAsync(int categoryId)
    {
        try
        {
            var category = await _unitOfWork.Repository<BlogPostCategory>().GetByIdAsync(categoryId);

            return category;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<BlogPostCategory?> UpdateBlogPostCategory(int categoryId, BlogPostCategory updatedcategory)
    {
        var category = await _unitOfWork.Repository<BlogPostCategory>().GetByIdAsync(categoryId);

        if (category == null) return null;

        if (updatedcategory == null || string.IsNullOrWhiteSpace(updatedcategory.Name))
            return null;

        category.Name = updatedcategory.Name;

        try
        {
            _unitOfWork.Repository<BlogPostCategory>().Update(category);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return category;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }
}