namespace MultaqaTech.Core.Entities.CourseDomainEntities;

public class Course : BaseEntityWithMediaUrl
{
    public int InstractorId { get; set; }
    public Instructor Instractor { get; set; } = new ();

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = new();

    public string Title { get; set; } = string.Empty;
    public string? Language { get; set; }

    // shadowing the picture url prop in base
    public string ThumbnailUrl
    {
        get { return base.MediaUrl; }
        set { base.MediaUrl = value; }
    }

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

    public CourseLevel Level { get; set; }
    public DeductionType DeductionType { get; set; }

    public List<Subject> Tags { get; set; } = new();
    public List<Subject> Prerequisites { get; set; } = new();
    public List<CourseReview>? Reviews { get; set; } = new();

    public List<CurriculumSection>? CurriculumSections { get; set; } = new();
    public List<StudentCourse>? StudentCourses { get; set; } = new();

    public List<string>? LearningObjectives { get; set; } = new();
    public List<string>? LecturesLinks { get; set; } = new();
    public List<int> EnrolledStudentsIds { get; set; } = new();

    [NotMapped] public List<int>? TagsIds { get; set; } = new();
    [NotMapped] public List<int>? PrerequisitesIds { get; set; } = new();
}