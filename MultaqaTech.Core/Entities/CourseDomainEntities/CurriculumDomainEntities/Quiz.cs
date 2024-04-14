namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class Quiz:CurriculumItem
{
    public List<Question>? Questions { get; set; } = new();

}
