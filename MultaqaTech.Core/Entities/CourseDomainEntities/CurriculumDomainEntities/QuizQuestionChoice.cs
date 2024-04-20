namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class QuizQuestionChoice : BaseEntity
{
    public string Content { get; set; }
    public string? Clarification { get; set; }
    public bool IsRight { get; set; }

    public int QuizQuestionId { get; set; }
    public QuizQuestion QuizQuestion { get; set; }
}
