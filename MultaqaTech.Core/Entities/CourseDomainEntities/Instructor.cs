namespace MultaqaTech.Core.Entities.CourseDomainEntities;

public class Instructor : BaseEntity
{
    public string? Bio { get; set; }
    public string? JobTitle { get; set; }

    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public List<Course>? Courses { get; set; }
}