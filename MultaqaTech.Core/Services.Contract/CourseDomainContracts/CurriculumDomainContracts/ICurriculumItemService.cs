
namespace MultaqaTech.Core.Services.Contract.CourseDomainContracts.CurriculumDomainContracts;

public interface ICurriculumItemService
{
    Task<ICurriculumItem?> CreateCurriculumItemAsync(ICurriculumItem curriculumItem);

    Task<IReadOnlyList<ICurriculumItem>> ReadAllAsync();

    Task<ICurriculumItem?> ReadByIdAsync(int curriculumItemId);

    Task<ICurriculumItem?> UpdateCurriculumItem(int curriculumItemId, ICurriculumItem CurriculumItem);

    Task<bool> DeleteCurriculumItem(int curriculumItemId);

    Task<IReadOnlyList<ICurriculumItem>> ReadCurriculumItemsByIds(List<int> curriculumItemIds);
}
