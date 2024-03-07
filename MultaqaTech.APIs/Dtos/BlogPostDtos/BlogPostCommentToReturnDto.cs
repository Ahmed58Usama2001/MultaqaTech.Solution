namespace MultaqaTech.APIs.Dtos.BlogPostDtos;

public class BlogPostCommentToReturnDto
{
    public int Id { get; set; }

    public string CommentContent { get; set; }

    public string? AuthorName { get; set; }

    public string DatePosted { get; set; }

    public int BlogPostId { get; set; }
    public string BlogPost { get; set; }
}
