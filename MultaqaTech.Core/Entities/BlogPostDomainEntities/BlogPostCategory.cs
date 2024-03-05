namespace MultaqaTech.Core.Entities.BlogPostDomainEntities;

public class BlogPostCategory : BaseEntity
{
    public string Name { get; set; }

    [JsonIgnore]
    public List<BlogPost>? BlogPosts { get; set; } = new();
}
