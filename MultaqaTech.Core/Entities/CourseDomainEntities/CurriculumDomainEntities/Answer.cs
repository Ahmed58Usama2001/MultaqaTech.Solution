namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class Answer:BaseEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsRight { get; set; }

    public int QuestionId { get; set; }
    public Question Question { get; set; }
}
