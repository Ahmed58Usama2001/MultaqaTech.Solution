namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class Note : BaseEntity
{
    public string Content { get; set; }

    public int WriterStudentId { get; set; }

    public DateTime PublishingDate { get; set; }=DateTime.Now;

    public int LectureId { get; set; }
    public Lecture Lecture { get; set; }
}