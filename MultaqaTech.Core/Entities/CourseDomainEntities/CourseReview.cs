namespace MultaqaTech.Core.Entities.CourseDomainEntities;

public class CourseReview : BaseEntity
{
    public int CourseId { get; set; }
    public Course Course { get; set; }

    public string? Content { get; set; }

    public int NumberOfStars { get; set; }

    public DateTime Date { get; set; }

    //public int StudentId { get; set; }
    //public AppUser Student { get; set; }
}