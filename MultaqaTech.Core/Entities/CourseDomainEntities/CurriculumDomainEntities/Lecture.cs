namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class Lecture:BaseEntity,ICurriculumItem
{
    public string Title { get; set; }
    public string? Description { get; set; }

    public string VideoUrl { get; set; }

    public int CurriculumSectionId { get; set; }
    public CurriculumSection CurriculumSection { get; set; }
}
