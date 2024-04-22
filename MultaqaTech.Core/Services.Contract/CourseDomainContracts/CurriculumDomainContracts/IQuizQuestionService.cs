namespace MultaqaTech.Core.Services.Contract.CourseDomainContracts.CurriculumDomainContracts;

public interface IQuizQuestionService
{
    Task<QuizQuestion?> CreateQuizQuestionAsync(QuizQuestion question);

    Task<IReadOnlyList<QuizQuestion>> ReadQuizQuestionsAsync(QuizQuestionSpeceficationsParams speceficationsParams);

    Task<QuizQuestion?> ReadByIdAsync(int questionId);

    Task<QuizQuestion?> UpdateQuizQuestion(int questionId, QuizQuestion updatedQuestion);

    Task<bool> DeleteQuizQuestion(int questionId);

    //Task<IReadOnlyList<CurriculumItem>> ReadCurriculumItemsByIds(List<int> curriculumItemIds);
}
