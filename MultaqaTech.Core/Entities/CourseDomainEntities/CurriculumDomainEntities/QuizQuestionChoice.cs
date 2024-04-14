namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class QuizQuestionChoice:BaseEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsRight { get; set; }

    public int QuizQuestionId { get; set; }
    public QuizQuestion QuizQuestion { get; set; }
}
