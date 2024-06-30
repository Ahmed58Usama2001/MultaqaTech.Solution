namespace MultaqaTech.APIs.Dtos.CourseDtos.CurriculumDtos.Q_ADtos.QuestionDtos;

public class QuestionReturnDto
{
    public int Id { get; set; }
    public DateTime PublishingDate { get; set; }

    public string Title { get; set; }

    public string Details { get; set; }

    public int LectureId { get; set; }

    public int AskerId { get; set; }
    public string AskerName { get; set; }

    public string? QuestionPictureUrl { get; set; }
}
