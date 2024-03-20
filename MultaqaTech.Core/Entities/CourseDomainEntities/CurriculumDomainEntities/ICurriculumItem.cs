namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public interface ICurriculumItem
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public int CurriculumSectionId { get; set; }
    public CurriculumSection CurriculumSection { get; set; }
}
