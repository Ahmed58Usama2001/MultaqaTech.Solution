namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class Question : BaseEntityWithMediaUrl
{
    public string Content { get; set; }

    public string? QuestionPictureUrl
    {
        get { return base.MediaUrl; }
        set { base.MediaUrl = value; }
    }

    public DateTime PublishingDate { get; set; }

    public string AskerId { get; set; }

    public int LectureId { get; set; }
    public Lecture Lecture { get; set; }

    public List<Answer>? Answers { get; set; } = new();
}
