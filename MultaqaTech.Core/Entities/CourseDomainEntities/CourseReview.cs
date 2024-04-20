namespace MultaqaTech.Core.Entities.CourseDomainEntities;

public class CourseReview : BaseEntityWithMediaUrl
{
    public int CourseId { get; set; }
    public int NumberOfStars { get; set; }

    public string? Content { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string ProfiePictureUrl { get => base.MediaUrl; set => base.MediaUrl = value; }

    public DateTime Date { get; set; } = DateTime.Now;
}