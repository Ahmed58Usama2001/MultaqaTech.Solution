namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class Question : BaseEntityWithMediaUrl
{
    public string Title { get; set; }


    public string Details { get; set; }

    public string? QuestionPictureUrl
    {
        get { return base.MediaUrl; }
        set { base.MediaUrl = value; }
    }

    public DateTime PublishingDate { get; set; }=DateTime.Now;

    public int AskerId { get; set; }

    public int LectureId { get; set; }
    public Lecture Lecture { get; set; }

    public List<Answer>? Answers { get; set; } = new();
}
