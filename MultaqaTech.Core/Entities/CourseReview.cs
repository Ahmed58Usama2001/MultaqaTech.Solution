namespace MultaqaTech.Core.Entities;

public class CourseReview : BaseEntity
{
    public string? ReviewContent { get; set; }

    public int NumberOfStars { get; set; }

    public DateTime Date { get; set; }

    //public int StudentId { get; set; }
    //public AppUser Student { get; set; }
}
