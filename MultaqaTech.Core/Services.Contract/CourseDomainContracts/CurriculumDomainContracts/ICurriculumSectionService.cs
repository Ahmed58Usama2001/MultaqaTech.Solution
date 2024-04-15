namespace MultaqaTech.Core.Services.Contract.CourseDomainContracts.CurriculumDomainContracts;

public interface ICurriculumSectionService
{
    Task<CurriculumSection?> CreateCurriculumSectionAsync(CurriculumSection curriculumSection);

    Task<IReadOnlyList<CurriculumSection>> ReadCurriculumSectionsAsync(CurriculumSectionSpeceficationsParams speceficationsParams);

    Task<CurriculumSection?> ReadByIdAsync(int curriculumSectionId);

    Task<CurriculumSection?> UpdateCurriculumSection(int curriculumSectionId, CurriculumSection updatedcurriculumSection);

    Task<bool> DeleteCurriculumSection(CurriculumSection curriculumSection);

    //Task<IReadOnlyList<CurriculumItem>> ReadCurriculumItemsByIds(List<int> curriculumItemIds);
}
