namespace MultaqaTech.APIs.Dtos;

public class CourseReviewDto
{
    [Required]
    public int CourseId { get; set; }

    [MaxLength(100, ErrorMessage = "Course Review must not ecxeed 500 characters")]
    [MinLength(3, ErrorMessage = "Course Review must be at least 3 characters long.")]
    public string? Content { get; set; }


    [Required]
    [Range(0, 5, ErrorMessage = "Rating Should be grater than or equal to zero and less than or equal to 5 !!")]
    public int NumberOfStars { get; set; }
}