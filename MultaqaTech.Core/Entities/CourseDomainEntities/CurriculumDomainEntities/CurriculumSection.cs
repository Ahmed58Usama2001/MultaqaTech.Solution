namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class CurriculumSection:BaseEntity
{
    public string Title { get; set; }

    public int Order { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }

    public List<Lecture>? Lectures { get; set; } = new();
    public List<Quiz>? Quizes { get; set; } = new();

}
