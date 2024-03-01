namespace MultaqaTech.Core.Entities;

public class BlogPostCategory:BaseEntity
{
    public string Name { get; set; }

    public List<BlogPost>? BlogPosts { get; set; } = new();
}
