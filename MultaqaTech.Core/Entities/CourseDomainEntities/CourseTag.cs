namespace MultaqaTech.Core.Entities.CourseDomainEntities;

public class CourseTag
{
    public int CourseId { get; set; }
    public Course Course { get; set; }

    public int TagId { get; set; }
    public Subject Tag { get; set; }

    public string TagName { get; set; }
}