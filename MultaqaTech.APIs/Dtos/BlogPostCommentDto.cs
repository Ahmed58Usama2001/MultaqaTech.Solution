namespace MultaqaTech.Core.Entities;

public class BlogPostCommentDto:BaseEntity
{
    public int Id { get; set; }

    public string CommentContent { get; set; }

    public string AuthorName { get; set; }

    public DateTime DatePosted { get; set; }

    public int BlogPostId { get; set; }
    public string BlogPost { get; set; }
}
