
namespace MultaqaTech.Service.CourseDomainServices.CurriculumDomainServices;

public class QuizQuestionService(IUnitOfWork unitOfWork) : IQuizQuestionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<QuizQuestion?> CreateQuizQuestionAsync(QuizQuestion question)
    {
        try
        {
            await _unitOfWork.Repository<QuizQuestion>().AddAsync(question);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return question;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteQuizQuestion(int questionId)
    {
        var question = await _unitOfWork.Repository<QuizQuestion>().GetByIdAsync(questionId);

        if (question == null)
            return false;

        try
        {
            _unitOfWork.Repository<QuizQuestion>().Delete(question);

            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                return false;

            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return false;
        }
    }

    public async Task<QuizQuestion?> ReadByIdAsync(int questionId)
    {
        var spec = new QuizQuestionWithIncludesSpecifications(questionId);

        var question = await _unitOfWork.Repository<QuizQuestion>().GetByIdWithSpecAsync(spec);

        return question;
    }

    public async Task<IReadOnlyList<QuizQuestion>> ReadQuizQuestionsAsync(QuizQuestionSpeceficationsParams speceficationsParams)
    {
        var spec = new QuizQuestionWithIncludesSpecifications(speceficationsParams);

        var questions = await _unitOfWork.Repository<QuizQuestion>().GetAllWithSpecAsync(spec);

        return questions;
    }

    public async Task<QuizQuestion?> UpdateQuizQuestion(int questionId, QuizQuestion updatedQuestion)
    {
        var question = await _unitOfWork.Repository<QuizQuestion>().GetByIdAsync(questionId);

        if (question == null || updatedQuestion == null || string.IsNullOrWhiteSpace(updatedQuestion.Content))
            return null;

        question = updatedQuestion;

        try
        {
            _unitOfWork.Repository<QuizQuestion>().Update(question);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return question;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }
}
