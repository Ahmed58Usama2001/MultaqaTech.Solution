namespace MultaqaTech.Core.Entities.BlogPostDomainEntities;

public class BlogPostComment : BaseEntity
{
    public string CommentContent { get; set; }

    public string? AuthorName { get; set; }

    public DateTime DatePosted { get; set; }

    public int BlogPostId { get; set; }
    public BlogPost BlogPost { get; set; }
}
