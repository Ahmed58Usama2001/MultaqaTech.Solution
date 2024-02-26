namespace MultaqaTech.APIs.Dtos;

public class SubjectDto
{
    [Required]
    [MaxLength(100, ErrorMessage = "The Subject Name must be less than 100 characters long.")]
    [MinLength(3, ErrorMessage = "The Subject Name must be at least 3 characters long.")]
    public string Name { get; set; }
}