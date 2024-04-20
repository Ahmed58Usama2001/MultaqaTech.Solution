namespace MultaqaTech.APIs.Dtos.BlogPostDtos;

public class BlogPostCreateDto
{
    public int CategoryId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;

    public List<int>? Tags { get; set; } = [];
}