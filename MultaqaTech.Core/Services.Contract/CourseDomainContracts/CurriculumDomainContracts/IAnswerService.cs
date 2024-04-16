namespace MultaqaTech.Core.Services.Contract.CourseDomainContracts.CurriculumDomainContracts;

public interface IAnswerService
{
    Task<Answer?> CreateAnswerAsync(Answer answer);

    Task<IReadOnlyList<Answer>> ReadAnswersAsync(AnswerSpeceficationsParams speceficationsParams);

    Task<Answer?> ReadByIdAsync(int answerId);

    Task<Answer?> UpdateAnswer(int answerId, Answer updatedAnswer);

    Task<bool> DeleteAnswer(int answerId);

    //Task<IReadOnlyList<CurriculumItem>> ReadCurriculumItemsByIds(List<int> curriculumItemIds);
}
