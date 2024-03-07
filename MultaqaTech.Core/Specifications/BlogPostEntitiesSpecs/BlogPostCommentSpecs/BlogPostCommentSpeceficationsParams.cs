namespace MultaqaTech.Core.Specifications.BlogPost_Specs;

public class BlogPostCommentSpeceficationsParams
{
    public string? sort { get; set; }

    public int? blogPostId { get; set; }


    private const int maxPageSize = 12;
    private int pageSize = 6;

    public int PageSize
    {
        get { return pageSize; }
        set { pageSize = value > maxPageSize ? maxPageSize : value; }
    }

    public int PageIndex { get; set; } = 1;

}
