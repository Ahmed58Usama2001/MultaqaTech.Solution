namespace MultaqaTech.APIs.Dtos.CourseDtos.CurriculumDtos.QuizQuestionDtos;

public class QuizQuestionReturnDto
{
    public int Id { get; set; }
    public string Content { get; set; }

    public int QuizId { get; set; }

    public List<QuizQuestionChoiceReturnDto>? QuizQuestionChoices { get; set; } = new();
}
