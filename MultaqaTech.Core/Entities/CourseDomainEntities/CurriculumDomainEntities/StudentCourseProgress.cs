namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class StudentCourseProgress : BaseEntity
{
    public int StudentCourseId { get; set; }

    public int LectureId { get; set; }

    public int QuizId { get; set; }

    public bool IsCompleted { get; set; }

}
