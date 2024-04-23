namespace MultaqaTech.Core.Entities.CourseDomainEntities;

public class Instractor : AppUser
{
    public string? Bio { get; set; }
    public string? JobTitle { get; set; }

    public DateTime MyProperty { get; set; }

    public List<Course>? Courses { get; set; }
}