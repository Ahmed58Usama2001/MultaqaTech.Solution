namespace MultaqaTech.Core.Specifications;

public class CourseSpecificationsParams
{
    public int? InstructorId { get; set; }
    public int? StudentId { get; set; }
    public string? Language { get; set; }

    public string? sort { get; set; }

    public int? SubjectId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public CourseLevel? CourseLevel{ get; set; }

    private const int maxPageSize = 12;
    private int pageSize = 6;

    public int PageSize
    {
        get { return pageSize; }
        set { pageSize = value > maxPageSize ? maxPageSize : value; }
    }

    public int PageIndex { get; set; } = 1;
}