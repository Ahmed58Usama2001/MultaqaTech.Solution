namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class CurriculumSection:BaseEntity
{

    public int CourseId { get; set; }
    public Course Course { get; set; }

    public List<Lecture>? CurriculumSectionLectures { get; set; } = new();
    public List<Quiz>? CurriculumSectionQuizes { get; set; } = new();

}
