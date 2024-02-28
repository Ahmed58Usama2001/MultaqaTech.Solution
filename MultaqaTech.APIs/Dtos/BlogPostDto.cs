namespace MultaqaTech.APIs.Dtos;

public class BlogPostDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string AuthorName { get; set; }

    public string Content { get; set; }

    public int CategoryId { get; set; }
    public string Category { get; set; }

    public string PublishingDate { get; set; }

    public int NumberOfViews { get; set; }

    public List<string>? Tags { get; set; } = new();

    public List<string>? Comments { get; set; } = new();
}
