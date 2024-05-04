
namespace MultaqaTech.APIs.Dtos.CourseDtos.CurriculumDtos.QuizQuestionDtos;

public class QuizQuestionCreateDto
{
    public string Content { get; set; }

    public int QuizId { get; set; }

    public List<QuizQuestionChoiceCreateDto>? QuizQuestionChoices { get; set; } = new();
}
