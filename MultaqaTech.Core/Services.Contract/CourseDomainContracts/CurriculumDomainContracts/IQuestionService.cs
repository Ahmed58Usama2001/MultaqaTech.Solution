namespace MultaqaTech.Core.Services.Contract.CourseDomainContracts.CurriculumDomainContracts;

public interface IQuestionService
{
    Task<Question?> CreateQuestionAsync(Question question);

    Task<IReadOnlyList<Question>> ReadQuestionsAsync(QuestionSpeceficationsParams speceficationsParams);

    Task<Question?> ReadByIdAsync(int questionId);

    public Task<Question?> UpdateQuestion(Question storedQuestion, Question newQuestion);


    Task<bool> DeleteQuestion(Question question);

    Task<int> GetCountAsync(QuestionSpeceficationsParams speceficationsParams);
}
