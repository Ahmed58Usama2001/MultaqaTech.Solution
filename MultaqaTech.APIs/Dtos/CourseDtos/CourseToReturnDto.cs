namespace MultaqaTech.APIs.Dtos;

public class CourseToReturnDto
{
    public int Id { get; set; }
    public int TotalEnrolled { get; set; }
    public int DeductionAmount { get; set; }
    public int NumberOfLectures { get; set; }

    public string? Language { get; set; }

    public string? InstructorName { get; set; }
    public string? InstructorPicture { get; set; }

    public string? ThumbnailUrl { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;

    public decimal Price { get; set; }
    public float Rating
    {
        get
        {
            return Reviews.Count > 0 ? ((float)Reviews.Sum(e => e.NumberOfStars)) / (float)Reviews.Count : 0;
        }
    }
    public decimal Duration { get; set; }

    public DateTime UploadDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }

    public CourseLevel Level { get; set; }

    public DeductionType DeductionType { get; set; }

    public List<string> Tags { get; set; } = [];

    public List<string> Prerequisites { get; set; } = [];

    public List<string>? LearningObjectives { get; set; } = [];

    public List<string>? LecturesLinks { get; set; } = [];

    public List<CourseReviewToReturnDto> Reviews { get; set; } = [];
}