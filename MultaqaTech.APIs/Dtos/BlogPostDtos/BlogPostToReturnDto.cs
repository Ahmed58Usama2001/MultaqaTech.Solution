namespace MultaqaTech.APIs.Dtos.BlogPostDtos;

public class BlogPostToReturnDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string AuthorName { get; set; }

    public string Content { get; set; }

    public string? PictureUrl { get; set; }


    public int CategoryId { get; set; }
    public string Category { get; set; }

    public string PublishingDate { get; set; }

    public int NumberOfViews { get; set; }

    public List<string>? Tags { get; set; } = new();
    public List<object>? Comments { get; set; } = new();
}
