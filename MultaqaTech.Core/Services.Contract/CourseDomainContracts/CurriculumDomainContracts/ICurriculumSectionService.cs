namespace MultaqaTech.Core.Services.Contract.CourseDomainContracts.CurriculumDomainContracts;

public interface ICurriculumSectionService
{
    Task<CurriculumSection?> CreateCurriculumSectionAsync(CurriculumSection curriculumSection);

    Task<IReadOnlyList<CurriculumSection>> ReadCurriculumSectionsAsync(CurriculumSectionSpeceficationsParams speceficationsParams);

    Task<CurriculumSection?> ReadByIdAsync(int curriculumSectionId);

    Task<CurriculumSection?> UpdateCurriculumSection(CurriculumSection storedCurriculumSection, CurriculumSection newCurriculumSection);


    Task<bool> DeleteCurriculumSection(CurriculumSection curriculumSection);

    Task<bool> ReorderSections(int courseId, List<int> newOrder);

    //Task<IReadOnlyList<CurriculumItem>> ReadCurriculumItemsByIds(List<int> curriculumItemIds);
}
