namespace MultaqaTech.Core.Entities.CourseDomainEntities;

public class Student : BaseEntity
{
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public List<StudentCourse>? StudentCourses { get; set; } = new();

}