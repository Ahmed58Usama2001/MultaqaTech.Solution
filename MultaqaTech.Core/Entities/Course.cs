using MultaqaTech.Core.Entities.Enums;

namespace MultaqaTech.Core.Entities;

public class Course : BaseEntity
{
    //public Instructor Instructor { get; set; }

    public Subject Subject { get; set; }

    public string Title { get; set; }
    public string? Language { get; set; }

    public decimal Price { get; set; }
    public decimal Rating { get; set; }
    public decimal Duration { get; set; }

    public int SubjectId { get; set; }
    public int TotalEnrolled { get; set; }
    public int NumberOfLectures { get; set; }

    public DateTime LastUpdatedDate { get; set; }
    public DateTime UploadDate { get; set; }

    public CourseLevel CourseLevel { get; set; }

    List<Subject> Tags { get; set; } = new();
    List<Subject> Prerequisites { get; set; } = new();
    List<CourseReview> CourseReviews { get; set; } = new();
    
    List<string>? LearningObjectives { get; set; } = new();
}