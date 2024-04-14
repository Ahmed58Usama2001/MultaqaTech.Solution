namespace MultaqaTech.Service.CourseDomainServices.CurriculumDomainServices;

public class CurriculumItemService(IUnitOfWork unitOfWork) : ICurriculumItemService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CurriculumItem?> CreateCurriculumItemAsync(CurriculumItem curriculumItem)
    {
        try
        {
            await _unitOfWork.Repository<CurriculumItem>().AddAsync(curriculumItem);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return curriculumItem;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteBlogPostCurriculumItem(int CurriculumItemId)
    {
        var CurriculumItem = await _unitOfWork.Repository<CurriculumItem>().GetByIdAsync(CurriculumItemId);

        if (CurriculumItem == null)
            return false;

        try
        {
            _unitOfWork.Repository<CurriculumItem>().Delete(CurriculumItem);

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

    public async Task<IReadOnlyList<CurriculumItem>> ReadAllAsync()
    {
        try
        {
            var CurriculumItems = await _unitOfWork.Repository<CurriculumItem>().GetAllAsync();

            return CurriculumItems;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<CurriculumItem?> ReadByIdAsync(int curriculumItemId)
    {
        try
        {
            var curriculumItem = await _unitOfWork.Repository<CurriculumItem>().GetByIdAsync(curriculumItemId);

            return curriculumItem;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<CurriculumItem?> UpdateCurriculumItem(int curriculumItemId, CurriculumItem updatedCurriculumItem)
    {
        var curriculumItem = await _unitOfWork.Repository<CurriculumItem>().GetByIdAsync(curriculumItemId);

        if (curriculumItem == null) return null;

        if (curriculumItem == null || updatedCurriculumItem == null || string.IsNullOrWhiteSpace(updatedCurriculumItem.Title))
            return null;

        curriculumItem = updatedCurriculumItem;

        try
        {
            _unitOfWork.Repository<CurriculumItem>().Update(curriculumItem);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return curriculumItem;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }
}
