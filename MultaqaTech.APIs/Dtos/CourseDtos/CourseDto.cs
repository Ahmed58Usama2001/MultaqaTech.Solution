namespace MultaqaTech.APIs.Dtos;

public class CourseDto
{
    [Required]
    public int SubjectId { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "Course Title must not exceed 100 characters")]
    [MinLength(3, ErrorMessage = "Course Title must be at least 3 characters long.")]
    public string Title { get; set; } = string.Empty;


    [MaxLength(50, ErrorMessage = "Course Language must not exceed 50 characters")]
    [MinLength(3, ErrorMessage = "Course Language must be at least 3 characters long.")]
    public string? Language { get; set; }

    [Required]
    public IFormFile Thumbnail { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Price Should be grater than or equal to zero !")]
    public decimal Price { get; set; }

    [Required]
    public CourseLevel Level { get; set; }

    //[Required]
    public List<int>? TagsIds { get; set; } = [];

    public List<int>? PrerequisitesIds { get; set; } = [];

    public List<string>? LearningObjectives { get; set; } = [];
}