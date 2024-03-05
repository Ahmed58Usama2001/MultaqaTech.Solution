namespace MultaqaTech.APIs.Dtos.BlogPostDtos;

public class BlogPostCommentCreateDto : BaseEntity
{
    public string CommentContent { get; set; }

    public string AuthorName { get; set; }

    public DateTime DatePosted { get; set; }

    public int BlogPostId { get; set; }
    public string BlogPost { get; set; }
}
