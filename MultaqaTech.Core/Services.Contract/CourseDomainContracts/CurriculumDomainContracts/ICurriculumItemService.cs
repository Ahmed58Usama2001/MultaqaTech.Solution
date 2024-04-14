namespace MultaqaTech.Core.Services.Contract.CourseDomainContracts.CurriculumDomainContracts;

public interface ICurriculumItemService
{
    Task<CurriculumItem?> CreateCurriculumItemAsync(CurriculumItem curriculumItem);

    Task<IReadOnlyList<CurriculumItem>> ReadAllAsync();

    Task<CurriculumItem?> ReadByIdAsync(int curriculumItemId);

    Task<CurriculumItem?> UpdateCurriculumItem(int curriculumItemId, CurriculumItem updatedCurriculumItem);

    Task<bool> DeleteBlogPostCurriculumItem(int CurriculumItemId);

    //Task<IReadOnlyList<CurriculumItem>> ReadCurriculumItemsByIds(List<int> curriculumItemIds);
}
