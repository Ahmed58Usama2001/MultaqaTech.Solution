namespace MultaqaTech.APIs.Dtos.CourseDtos.CurriculumDtos.Q_ADtos.QuestionDtos;

public class QuestionUpdateDto
{
    public string Title { get; set; }

    public string Details { get; set; }

    public IFormFile? QuestionPicture { get; set; }
}