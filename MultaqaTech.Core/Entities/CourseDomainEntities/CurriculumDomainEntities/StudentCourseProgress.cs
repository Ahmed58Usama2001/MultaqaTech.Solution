namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class StudentCourseProgress : BaseEntity
{
    public int StudentCourseId { get; set; }
    public StudentCourse StudentCourse { get; set; }

    public int LectureId { get; set; }
    public Lecture Lecture { get; set; }

    public int QuizId { get; set; }
    public Quiz Quiz { get; set; }

    public bool IsCompleted { get; set; }

}
