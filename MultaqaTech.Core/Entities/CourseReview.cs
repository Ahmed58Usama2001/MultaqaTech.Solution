namespace MultaqaTech.Core.Entities;

public class CourseReview : BaseEntity
{
    public string? Comment { get; set; }
    public int NumberOfStars { get; set; }
    public DateTime Date { get; set; }
    //public Student Student { get; set; }
}
