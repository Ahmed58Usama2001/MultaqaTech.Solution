namespace MultaqaTech.Core.Entities.CourseDomainEntities;

public class CourseToReturnDto
{
    public int Id { get; set; }
    public string Subject { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? Instructor { get; set; }

    public decimal Rating { get; set; }
    public decimal Duration { get; set; }
    public decimal Price { get; set; }

    public int TotalEnrolled { get; set; }
    public int NumberOfLectures { get; set; }
    public int DeductionAmount { get; set; }

    public DateTime LastUpdatedDate { get; set; }
    public DateTime UploadDate { get; set; }

    public CourseLevel CourseLevel { get; set; }
    public DeductionType DeductionType { get; set; }

    public List<string> Tags { get; set; } = new();
    public List<string> Prerequisites { get; set; } = new();
    //public List<CourseReview> CourseReviews { get; set; } = new();

    public List<string>? LearningObjectives { get; set; } = new();
    public List<string>? LecturesLinks { get; set; } = new();
}