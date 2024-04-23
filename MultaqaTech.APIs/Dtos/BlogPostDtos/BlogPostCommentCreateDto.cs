namespace MultaqaTech.APIs.Dtos.BlogPostDtos;

public class BlogPostCommentCreateDto
{
    public string CommentContent { get; set; } = string.Empty;

    public int BlogPostId { get; set; }
}