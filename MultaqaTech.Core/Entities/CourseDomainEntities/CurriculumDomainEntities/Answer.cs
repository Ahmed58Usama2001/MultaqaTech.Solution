namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class Answer : BaseEntity
{
    public string Content { get; set; }

    public int AnswererId { get; set; }

    public DateTime PublishingDate { get; set; } = DateTime.Now;

    public int QuestionId { get; set; }
    public Question Question { get; set; }
}