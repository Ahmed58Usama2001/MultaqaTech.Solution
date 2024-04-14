namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class Note:BaseEntity
{
    public string Description { get; set; }

    public int LectureId { get; set; }
    public Lecture Lecture { get; set; }
}
