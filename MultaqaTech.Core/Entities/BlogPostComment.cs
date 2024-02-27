namespace MultaqaTech.Core.Entities;

public class BlogPostComment:BaseEntity
{
    public string Content { get; set; }

    //public int AuthorId { get; set; }
    //public AppUser Author { get; set; }

    public DateTime DatePosted { get; set; }

    public int BlogPostId { get; set; }
    public BlogPost BlogPost { get; set; }
}
