namespace MultaqaTech.Service.CourseDomainServices.CurriculumDomainServices;

public class CurriculumSectionService(IUnitOfWork unitOfWork) : ICurriculumSectionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CurriculumSection?> CreateCurriculumSectionAsync(CurriculumSection curriculumSection)
    {
        try
        {
            await _unitOfWork.Repository<CurriculumSection>().AddAsync(curriculumSection);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return curriculumSection;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteCurriculumSection(CurriculumSection curriculumSection)
    {
        try
        {
            _unitOfWork.Repository<CurriculumSection>().Delete(curriculumSection);

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

    public async Task<CurriculumSection?> ReadByIdAsync(int curriculumSectionId)
    {
        var spec = new CurriculumSectionWithIncludesSpecifications(curriculumSectionId);

        var curriculumSection = await _unitOfWork.Repository<CurriculumSection>().GetByIdWithSpecAsync(spec);

        return curriculumSection;
    }

    public async Task<IReadOnlyList<CurriculumSection>> ReadCurriculumSectionsAsync(CurriculumSectionSpeceficationsParams speceficationsParams)
    {
        var spec = new CurriculumSectionWithIncludesSpecifications(speceficationsParams);

        var curriculumSections = await _unitOfWork.Repository<CurriculumSection>().GetAllWithSpecAsync(spec);

        return curriculumSections;
    }

    public async Task<CurriculumSection?> UpdateCurriculumSection(int curriculumSectionId, CurriculumSection updatedcurriculumSection)
    {
        var curriculumSection = await _unitOfWork.Repository<CurriculumSection>().GetByIdAsync(curriculumSectionId);

        if (curriculumSection == null || updatedcurriculumSection == null || string.IsNullOrWhiteSpace(updatedcurriculumSection.Title))
            return null;

        curriculumSection = updatedcurriculumSection;

        try
        {
            _unitOfWork.Repository<CurriculumSection>().Update(curriculumSection);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return curriculumSection;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }
}
