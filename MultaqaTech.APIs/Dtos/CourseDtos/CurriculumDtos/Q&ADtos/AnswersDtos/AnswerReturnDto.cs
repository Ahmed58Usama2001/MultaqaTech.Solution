namespace MultaqaTech.APIs.Dtos.CourseDtos.CurriculumDtos.Q_ADtos.AnswersDtos;

public class AnswerReturnDto
{
    public int Id { get; set; }
    public DateTime PublishingDate { get; set; }

    public string Content { get; set; }

    public int AnswererId { get; set; }
    public string AnswererName { get; set; }

    public int QuestionId { get; set; }

}
