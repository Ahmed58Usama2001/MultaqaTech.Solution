using System.ComponentModel.DataAnnotations.Schema;

namespace MultaqaTech.Core.Entities.CourseDomainEntities;

public class Course : BaseEntity
{
    //public int InstructorId { get; set; }
    //public AppUser Instructor { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Language { get; set; }

    public decimal Price { get; set; }
    public decimal Rating { get; set; }
    public decimal Duration { get; set; }

    public int TotalEnrolled { get; set; }

    [NotMapped]
    public int NumberOfLectures
    {
        get { return LecturesLinks?.Count ?? 0; }
    }
    public int DeductionAmount { get; set; }

    public DateTime LastUpdatedDate { get; set; }
    public DateTime UploadDate { get; set; }

    public CourseLevel CourseLevel { get; set; }
    public DeductionType DeductionType { get; set; }

    List<Subject> Tags { get; set; } = new();
    List<Subject> Prerequisites { get; set; } = new();
    List<CourseReview> CourseReviews { get; set; } = new();

    List<string>? LearningObjectives { get; set; } = new();
    List<string>? LecturesLinks { get; set; } = new();
    List<int> EnrolledStudentsIds { get; set; } = new();
}