namespace MultaqaTech.Core.Services.Contract.CourseDomainContracts.CurriculumDomainContracts;

public interface IQuestionService
{
    Task<Question?> CreateQuestionAsync(Question question);

    Task<IReadOnlyList<Question>> ReadQuestionsAsync(QuestionSpeceficationsParams speceficationsParams);

    Task<Question?> ReadByIdAsync(int questionId);

    Task<Question?> UpdateQuestion(int questionId, Question updatedQuestion);

    Task<bool> DeleteQuestion(int questionId);

    //Task<IReadOnlyList<CurriculumItem>> ReadCurriculumItemsByIds(List<int> curriculumItemIds);
}
