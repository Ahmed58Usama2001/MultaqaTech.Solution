namespace MultaqaTech.Core.Services.Contract.CourseDomainContracts.CurriculumDomainContracts;

public interface ICurriculumItemService
{
    Task<CurriculumItem?> CreateCurriculumItemAsync(CurriculumItem curriculumItem);

    Task<IReadOnlyList<CurriculumItem>> ReadCurriculumItemsbyTypeAsync(CurriculumItemSpeceficationsParams speceficationsParams,CurriculumItemType type);
    Task<IReadOnlyList<CurriculumItem>> ReadCurriculumItemsAsync(CurriculumItemSpeceficationsParams speceficationsParams);

    Task<CurriculumItem?> ReadByIdAsync(int curriculumItemId, CurriculumItemType type);

    Task<CurriculumItem?> UpdateCurriculumItem(CurriculumItem storedItem, CurriculumItem newItem);

    Task<bool> UpdateCurriculumItemCompletionStateInStudentProgress(int CurriculumItemId, int studentCourseId,CurriculumItemType type);


    Task<bool> DeleteCurriculumItem(CurriculumItem CurriculumItem);

    Task<bool> ReorderItems(int sectionId, List<int> newOrder);


    //Task<IReadOnlyList<CurriculumItem>> ReadCurriculumItemsByIds(List<int> curriculumItemIds);
}
