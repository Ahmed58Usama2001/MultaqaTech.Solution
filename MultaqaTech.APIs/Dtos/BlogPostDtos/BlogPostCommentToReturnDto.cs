namespace MultaqaTech.APIs.Dtos.BlogPostDtos;

public class BlogPostCommentToReturnDto
{
    public int Id { get; set; }
    public int BlogPostId { get; set; }

    public string? AuthorName { get; set; }
    public string BlogPost { get; set; } = string.Empty;
    public DateTime DatePosted { get; set; } 
    public string CommentContent { get; set; } = string.Empty;
}