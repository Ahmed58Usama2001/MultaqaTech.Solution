namespace MultaqaTech.Core.Entities.CourseDomainEntities;

public class CourseReview : BaseEntityWithMediaUrl
{
    public int CourseId { get; set; }

    public string? Content { get; set; }

    public int NumberOfStars { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public string StudentName { get; set; }
    public string ProfiePictureUrl { get => base.MediaUrl; set => base.MediaUrl = value; }

    //public int StudentId { get; set; }
    //public AppUser Student { get; set; }
}