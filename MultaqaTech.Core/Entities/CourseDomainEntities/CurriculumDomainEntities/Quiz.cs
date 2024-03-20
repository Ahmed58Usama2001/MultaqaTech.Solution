
namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class Quiz:BaseEntity,ICurriculumItem
{
    public string Title { get; set; }
    public string? Description { get; set; }

    public CurriculumItemType CurriculumItemType { get; set; } = CurriculumItemType.Quiz;

    public int CurriculumSectionId { get; set; }
    public CurriculumSection CurriculumSection { get; set; }

    public List<Question>? Questions { get; set; } = new();

}
