namespace MultaqaTech.APIs.Dtos.CourseDtos.CurriculumDtos.QuizQuestionDtos;

public class QuizQuestionUpdateDto
{
    public string Content { get; set; }

    public List<QuizQuestionChoiceCreateDto>? QuizQuestionChoices { get; set; } = new();
}
