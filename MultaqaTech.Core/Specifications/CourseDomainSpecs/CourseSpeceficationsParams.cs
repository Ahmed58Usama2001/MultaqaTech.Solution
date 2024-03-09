namespace MultaqaTech.Core.Specifications;

public class CourseSpeceficationsParams
{
    public string? instractorId { get; set; }
    public string? StudentId { get; set; }

    private const int maxPageSize = 12;
    private int pageSize = 6;

    public int PageSize
    {
        get { return pageSize; }
        set { pageSize = value > maxPageSize ? maxPageSize : value; }
    }

    public int PageIndex { get; set; } = 1;
}