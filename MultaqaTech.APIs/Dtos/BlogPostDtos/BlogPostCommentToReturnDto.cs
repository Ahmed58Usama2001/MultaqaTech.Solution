namespace MultaqaTech.APIs.Dtos.BlogPostDtos;

public class BlogPostCommentToReturnDto
{
    public int Id { get; set; }
    public int BlogPostId { get; set; }

    public string? AuthorName { get; set; }
    public string BlogPost { get; set; } = string.Empty;
    public string DatePosted { get; set; } = string.Empty;
    public string CommentContent { get; set; } = string.Empty;
}