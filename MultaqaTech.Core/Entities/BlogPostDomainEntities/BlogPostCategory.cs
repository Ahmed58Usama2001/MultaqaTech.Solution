namespace MultaqaTech.Core.Entities.BlogPostDomainEntities;

public class BlogPostCategory : BaseEntity
{
    public string Name { get; set; }

    public List<BlogPost>? BlogPosts { get; set; } = new();
}
