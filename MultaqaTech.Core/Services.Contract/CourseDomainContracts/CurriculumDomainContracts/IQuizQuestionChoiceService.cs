namespace MultaqaTech.Core.Services.Contract.CourseDomainContracts.CurriculumDomainContracts;

public interface IQuizQuestionChoiceService
{
    Task<QuizQuestionChoice?> CreateQuizQuestionChoiceAsync(QuizQuestionChoice choice);

    Task<IReadOnlyList<QuizQuestionChoice>> ReadQuizQuestionChoicesAsync(QuizQuestionChoiceSpeceficationsParams speceficationsParams);

    Task<QuizQuestionChoice?> ReadByIdAsync(int choiceId);

    Task<QuizQuestionChoice?> UpdateQuizQuestionChoice(int choiceId, QuizQuestionChoice updatedChoice);

    Task<bool> DeleteQuizQuestionChoice(int choiceId);

    //Task<IReadOnlyList<CurriculumItem>> ReadCurriculumItemsByIds(List<int> curriculumItemIds);
}
