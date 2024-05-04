namespace MultaqaTech.APIs.Dtos.CourseDtos.CurriculumDtos.QuizQuestionChoiceDtos;

public class QuizQuestionChoiceCreateDto
{
    public string Content { get; set; }
    public string? Clarification { get; set; }
    public bool IsRight { get; set; }
}
