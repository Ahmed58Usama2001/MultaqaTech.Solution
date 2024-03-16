namespace MultaqaTech.Core.Entities.CourseDomainEntities;

public class CourseReview : BaseEntityWithPictureUrl
{
    public int CourseId { get; set; }

    public string? Content { get; set; }

    public int NumberOfStars { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public string StudentName { get; set; }
    public string ProfiePictureUrl { get => base.PictureUrl; set => base.PictureUrl = value; }

    //public int StudentId { get; set; }
    //public AppUser Student { get; set; }
}