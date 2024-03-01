using MultaqaTech.Core.Entities.Enums;

namespace MultaqaTech.APIs.Dtos;

public class CourseDto
{
    //public int InstructorId { get; set; }

    //public int Id { get; set; }
    [Required]
    public int SubjectId { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "Course Title must not ecxeed 100 characters")]
    [MinLength(3, ErrorMessage = "Course Title must be at least 3 characters long.")]
    public string Title { get; set; } = string.Empty;


    [MaxLength(50, ErrorMessage = "Course Language must not ecxeed 50 characters")]
    [MinLength(3, ErrorMessage = "Course Language must be at least 3 characters long.")]
    public string? Language { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Price Should be grater than or equal to zero !")]
    public decimal Price { get; set; }

    [Required]
    public CourseLevel CourseLevel { get; set; }

    //[Required]
    List<Subject> Tags { get; set; } = new();

    List<Subject> Prerequisites { get; set; } = new();

    List<string>? LearningObjectives { get; set; } = new();
}