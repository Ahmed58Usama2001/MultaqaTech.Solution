namespace MultaqaTech.Core.Services.Contract.CourseDomainContracts.CurriculumDomainContracts;

public interface ICurriculumItemService
{
    Task<CurriculumItem?> CreateCurriculumItemAsync(CurriculumItem curriculumItem);

    Task<IReadOnlyList<CurriculumItem>> ReadCurriculumSectionsAsync(CurriculumItemSpeceficationsParams speceficationsParams);

    Task<CurriculumItem?> ReadByIdAsync(int curriculumItemId);

    Task<CurriculumItem?> UpdateCurriculumItem(int curriculumItemId, CurriculumItem updatedCurriculumItem);

    Task<bool> DeleteCurriculumItem(int CurriculumItemId);

    //Task<IReadOnlyList<CurriculumItem>> ReadCurriculumItemsByIds(List<int> curriculumItemIds);
}
