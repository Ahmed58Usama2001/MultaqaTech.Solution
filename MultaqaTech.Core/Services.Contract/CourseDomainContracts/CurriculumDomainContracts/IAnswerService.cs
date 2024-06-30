namespace MultaqaTech.Core.Services.Contract.CourseDomainContracts.CurriculumDomainContracts;

public interface IAnswerService
{
    Task<Answer?> CreateAnswerAsync(Answer answer);

    Task<IReadOnlyList<Answer>> ReadAnswersAsync(AnswerSpeceficationsParams speceficationsParams);

    Task<Answer?> ReadByIdAsync(int answerId);

    public Task<Answer?> UpdateAnswer(Answer storedAnswer, Answer newAnswer);

    Task<bool> DeleteAnswer(int answerId);

    Task<int> GetCountAsync(AnswerSpeceficationsParams speceficationsParams);
}
