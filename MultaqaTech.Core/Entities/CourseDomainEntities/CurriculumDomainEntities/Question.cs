namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class Question:BaseEntityWithPictureUrl
{
    public string Title { get; set; }

    public string? QuestionPictureUrl
    {
        get { return base.PictureUrl; }
        set { base.PictureUrl = value; }
    }

    public int QuizId { get; set; }
    public Quiz Quiz { get; set; }

    public List<Answer>? Answers { get; set; } = new();
}
