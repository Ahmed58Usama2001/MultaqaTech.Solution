namespace MultaqaTech.APIs.Dtos.CourseDtos.CurriculumDtos.QuizQuestionChoiceDtos;

public class QuizQuestionChoiceReturnDto
{
    public int Id { get; set; }

    public string Content { get; set; }

    public bool IsRight { get; set; }

    public string? Clarification { get; set; }
}
