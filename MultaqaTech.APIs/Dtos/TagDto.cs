namespace MultaqaTech.APIs.Dtos;

public class TagDto
{
    [Required]
    [MaxLength(100, ErrorMessage = "The Tag Name must be less than 100 characters long.")]
    [MinLength(3, ErrorMessage = "The Tag Name must be at least 3 characters long.")]
    public string Name { get; set; }
}