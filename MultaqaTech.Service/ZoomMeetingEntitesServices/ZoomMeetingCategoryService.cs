namespace MultaqaTech.Service.ZoomMeetingEntitesServices;

public class ZoomMeetingCategoryService(IUnitOfWork unitOfWork) : IZoomMeetingCategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ZoomMeetingCategory?> CreateZoomMeetingCategoryAsync(ZoomMeetingCategory category)
    {
        try
        {
            await _unitOfWork.Repository<ZoomMeetingCategory>().AddAsync(category);
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

    public async Task<bool> DeleteZoomMeetingCategory(int categoryId)
    {
        var category = await _unitOfWork.Repository<ZoomMeetingCategory>().GetByIdAsync(categoryId);

        if (category == null)
            return false;

        try
        {
            _unitOfWork.Repository<ZoomMeetingCategory>().Delete(category);

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
/*
    public async Task<IReadOnlyList<ZoomMeetingCategory>> ReadAllAsync()
    {
        try
        {
            var categories = await _unitOfWork.Repository<ZoomMeetingCategory>().GetAllAsync();

            return categories;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }
*/
    public async Task<ZoomMeetingCategory?> ReadByIdAsync(int categoryId)
    {
        try
        {
            var category = await _unitOfWork.Repository<ZoomMeetingCategory>().GetByIdAsync(categoryId);

            return category;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<ZoomMeetingCategory?> UpdateZoomMeetingCategory(int categoryId, ZoomMeetingCategory updatedcategory)
    {
        var category = await _unitOfWork.Repository<ZoomMeetingCategory>().GetByIdAsync(categoryId);

        if (category == null) return null;

        if (updatedcategory == null || string.IsNullOrWhiteSpace(updatedcategory.Name))
            return null;

        category.Name = updatedcategory.Name;

        try
        {
            _unitOfWork.Repository<ZoomMeetingCategory>().Update(category);
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
