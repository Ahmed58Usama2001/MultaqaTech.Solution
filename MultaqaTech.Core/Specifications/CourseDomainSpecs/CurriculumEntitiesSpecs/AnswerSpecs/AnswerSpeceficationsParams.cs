namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class AnswerSpeceficationsParams
{
    public int? questionId { get; set; }
    public string? answererId { get; set; }

    public string? sort { get; set; }


    private const int maxPageSize = 10;
    private int pageSize = 5;

    public int PageSize
    {
        get { return pageSize; }
        set { pageSize = value > maxPageSize ? maxPageSize : value; }
    }

    public int PageIndex { get; set; } = 1;

}
