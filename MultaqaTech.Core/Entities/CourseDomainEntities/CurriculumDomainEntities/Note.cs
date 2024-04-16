namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class Note:BaseEntity
{
    public string Description { get; set; }

    public string WriterStudentId { get; set; }

    public DateTime PublishingDate { get; set; } = DateTime.Now;

    public int LectureId { get; set; }
    public Lecture Lecture { get; set; }
}
