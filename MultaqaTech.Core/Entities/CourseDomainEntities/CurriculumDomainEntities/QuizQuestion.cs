namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class QuizQuestion:BaseEntity
{
    public string Title { get; set; }

    public int QuizId { get; set; }
    public Quiz Quiz { get; set; }

    public List<QuizQuestionChoice>? QuizQuestionChoices { get; set; } = new();
}
