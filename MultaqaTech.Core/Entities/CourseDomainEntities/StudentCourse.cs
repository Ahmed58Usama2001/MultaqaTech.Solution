namespace MultaqaTech.Core.Entities.CourseDomainEntities;

public class StudentCourse : BaseEntity
{
    public int StudentId { get; set; }
    public Student Student { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }

    public int CompletionPercentage { get; set; }
}