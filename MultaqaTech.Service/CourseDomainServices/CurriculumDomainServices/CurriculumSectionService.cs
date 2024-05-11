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
            int changes = 0;
            _unitOfWork.Repository<CurriculumSection>().Delete(curriculumSection);

            CurriculumSectionSpeceficationsParams speceficationsParams = new CurriculumSectionSpeceficationsParams
            {
                courseId = curriculumSection.CourseId
            };

            var remainingSections = await ReadCurriculumSectionsAsync(speceficationsParams);

            foreach (var section in remainingSections.Where(s => s.Order > curriculumSection.Order))
            {
                section.Order--;
                changes++;
            }

            var result = await _unitOfWork.CompleteAsync();

            if (result <= changes)
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

    public async Task<bool> ReorderSections(int courseId, List<int> newOrder)
    {
        var speceficationsParams = new CurriculumSectionSpeceficationsParams
        {
            courseId = courseId
        };
        var spec = new CurriculumSectionWithIncludesSpecifications(speceficationsParams);
        var sections = await _unitOfWork.Repository<CurriculumSection>().GetAllWithSpecAsync(spec);

        for (int i = 0; i < newOrder.Count; i++)
        {
            var sectionId = newOrder[i];
            CurriculumSection? section = sections.FirstOrDefault(s => s.Id == sectionId);
            if (section != null)    
                section.Order = i + 1; // Adjust order starting from 1 (optional)

            try
            {
                _unitOfWork.Repository<CurriculumSection>().Update(section);               
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return false;
            }
        }

        var result = await _unitOfWork.CompleteAsync();
        if (result <= newOrder.Count) return false;

        return true;
    }

    public async Task<CurriculumSection?> UpdateCurriculumSection(CurriculumSection storedCurriculumSection, CurriculumSection newCurriculumSection)
    {
        if (newCurriculumSection == null || storedCurriculumSection == null)
            return null;

        storedCurriculumSection.Title = newCurriculumSection.Title;
        storedCurriculumSection.Objectives = newCurriculumSection.Objectives;

        try
        {
            _unitOfWork.Repository<CurriculumSection>().Update(storedCurriculumSection);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return storedCurriculumSection;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }
}
