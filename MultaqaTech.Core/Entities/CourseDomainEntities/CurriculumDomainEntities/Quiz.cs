namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class Quiz:CurriculumItem
{
    public string? QuizQuestionPictureUrl
    {
        get { return base.MediaUrl; }
        set { base.MediaUrl = value; }
    }

    public CurriculumItemType CurriculumItemType { get; set; } = CurriculumItemType.Quiz;

    public List<QuizQuestion>? QuizQuestions { get; set; } = new();

}
