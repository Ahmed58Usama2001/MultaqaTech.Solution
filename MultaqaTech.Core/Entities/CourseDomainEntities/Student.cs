namespace MultaqaTech.Core.Entities.CourseDomainEntities;

public class Student : AppUser
{
    public List<Course>? Courses { get; set; }
}