namespace MultaqaTech.Service.CourseDomainServices.CurriculumDomainServices;

public class CurriculumItemService(IUnitOfWork unitOfWork) : ICurriculumItemService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CurriculumItem?> CreateCurriculumItemAsync(CurriculumItem curriculumItem)
    {
        try
        {
            switch (curriculumItem?.CurriculumItemType)
            {
                case CurriculumItemType.Lecture:
                    await _unitOfWork.Repository<Lecture>().AddAsync((Lecture)curriculumItem);
                    break;

                case CurriculumItemType.Quiz:
                    await _unitOfWork.Repository<Quiz>().AddAsync((Quiz)curriculumItem);
                    break;
            }

            
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

    public async Task<bool> DeleteCurriculumItem(int CurriculumItemId)
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

    public async Task<CurriculumItem?> ReadByIdAsync(int curriculumItemId)
    {
        var curriculumItem = await _unitOfWork.Repository<CurriculumItem>().GetByIdAsync(curriculumItemId);

        object? spec = null;

        switch (curriculumItem?.CurriculumItemType)
        {
            case CurriculumItemType.Lecture:
                spec = new LectureWithIncludesSpecifications(curriculumItemId);
                break;

            case CurriculumItemType.Quiz:
                spec = new QuizWithIncludesSpecifications(curriculumItemId);
                break;
        }

        var curriculumItemWithIncludes = await _unitOfWork.Repository<CurriculumItem>().GetByIdWithSpecAsync((ISpecifications<CurriculumItem>)spec);

        return curriculumItemWithIncludes??null;
    }

    public async Task<IReadOnlyList<CurriculumItem>> ReadCurriculumSectionsAsync(CurriculumItemSpeceficationsParams speceficationsParams)
    {
        var curriculumItems = await _unitOfWork.Repository<CurriculumItem>().GetAllAsync();

        List<CurriculumItem>? curriculumItemsWihIncludes=null;
        CurriculumItem curriculumItemWithIncludes;

        object? spec = null;
        foreach (var curriculumItem in curriculumItems)
        {
            switch (curriculumItem?.CurriculumItemType)
            {
                case CurriculumItemType.Lecture:
                    spec = new LectureWithIncludesSpecifications(speceficationsParams);
                    curriculumItemWithIncludes = await _unitOfWork.Repository<Lecture>().GetByIdWithSpecAsync((ISpecifications<Lecture>)spec);
                    curriculumItemsWihIncludes?.Add(curriculumItemWithIncludes);
                    break;

                case CurriculumItemType.Quiz:
                    spec = new QuizWithIncludesSpecifications(speceficationsParams);
                    curriculumItemWithIncludes = await _unitOfWork.Repository<Quiz>().GetByIdWithSpecAsync((ISpecifications<Quiz>)spec);
                    curriculumItemsWihIncludes?.Add(curriculumItemWithIncludes);
                    break;
            }
        }

        return curriculumItemsWihIncludes ?? null;
    }

    public async Task<CurriculumItem?> UpdateCurriculumItem(int curriculumItemId, CurriculumItem updatedCurriculumItem)
    {
        var curriculumItem = await _unitOfWork.Repository<CurriculumItem>().GetByIdAsync(curriculumItemId);

        if (curriculumItem == null || updatedCurriculumItem == null || string.IsNullOrWhiteSpace(updatedCurriculumItem.Title))
            return null;

        curriculumItem = updatedCurriculumItem;

        try
        {
            switch (curriculumItem?.CurriculumItemType)
            {
                case CurriculumItemType.Lecture:
                    _unitOfWork.Repository<Lecture>().Update((Lecture)curriculumItem);
                    break;

                case CurriculumItemType.Quiz:
                    _unitOfWork.Repository<Quiz>().Update((Quiz)curriculumItem);
                    break;
            }

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
