using MultaqaTech.Core.Entities.SettingsEntities;

namespace MultaqaTech.Core.Entities.BlogPostDomainEntities;

public class BlogPost : BaseEntityWithPictureUrl
{
    public string Title { get; set; } = string.Empty;
    public string? AuthorName { get; set; }

    public string Content { get; set; } = string.Empty;

    public string? PostPictureUrl
    {
        get { return base.PictureUrl; }
        set { base.PictureUrl = value; }
    }

    public int BlogPostCategoryId { get; set; }
    public BlogPostCategory Category { get; set; }

    public DateTime PublishingDate { get; set; }

    public int NumberOfViews { get; set; }


    public List<Subject>? Tags { get; set; } = new();

    public List<BlogPostComment>? Comments { get; set; } = new();
}
