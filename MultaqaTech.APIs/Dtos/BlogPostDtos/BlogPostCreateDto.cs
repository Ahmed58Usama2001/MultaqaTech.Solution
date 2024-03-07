namespace MultaqaTech.APIs.Dtos.BlogPostDtos;

public class BlogPostCreateDto
{
    public string Title { get; set; }

    public string Content { get; set; }

    public string PictureUrl { get; set; }

    public int CategoryId { get; set; }

    public List<int>? Tags { get; set; } = new();
}
