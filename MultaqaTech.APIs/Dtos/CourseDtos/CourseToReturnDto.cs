using MultaqaTech.Core.Entities.Enums;

namespace MultaqaTech.Core.Entities.CourseDomainEntities;

public class CourseToReturnDto
{
    public string SubjectName { get; set; }

    public decimal Rating { get; set; }
    public decimal Duration { get; set; }

    public int TotalEnrolled { get; set; }
    public int NumberOfLectures { get; set; }
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
}