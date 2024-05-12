namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class QuestionSpeceficationsParams
{
    public int? lectureId { get; set; }

    public string? sort { get; set; }

    private string? search;

    public string? Search
    {
        get { return search; }
        set { search = value?.ToLower(); }
    }

    private const int maxPageSize = 5;
    private int pageSize = 3;

    public int PageSize
    {
        get { return pageSize; }
        set { pageSize = value > maxPageSize ? maxPageSize : value; }
    }

    public int PageIndex { get; set; } = 1;

}
