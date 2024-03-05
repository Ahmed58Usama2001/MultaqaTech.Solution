namespace MultaqaTech.APIs.Dtos.BlogPostDtos;

public class BlogPostCreateDto
{
    public string Title { get; set; }

    public string AuthorName { get; set; }

    public string Content { get; set; }

    public int CategoryId { get; set; }
    public string Category { get; set; }

    public string PublishingDate { get; set; }

    public int NumberOfViews { get; set; }

    public List<SubjectCreateDto>? Tags { get; set; } = new();
}
