namespace MultaqaTech.Service.CourseDomainServices.CurriculumDomainServices;

public class QuestionSectionService(IUnitOfWork unitOfWork) : IQuestionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Question?> CreateQuestionAsync(Question question)
    {
        try
        {
            await _unitOfWork.Repository<Question>().AddAsync(question);
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

    public async Task<bool> DeleteQuestion(int questionId)
    {
        var question = await _unitOfWork.Repository<Question>().GetByIdAsync(questionId);

        if (question == null)
            return false;

        try
        {
            _unitOfWork.Repository<Question>().Delete(question);

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

    public async Task<Question?> ReadByIdAsync(int questionId)
    {
        var spec = new QuestionWithIncludesSpecifications(questionId);

        var question = await _unitOfWork.Repository<Question>().GetByIdWithSpecAsync(spec);

        return question;
    }

    public async Task<IReadOnlyList<Question>> ReadQuestionsAsync(QuestionSpeceficationsParams speceficationsParams)
    {
        var spec = new QuestionWithIncludesSpecifications(speceficationsParams);

        var questions = await _unitOfWork.Repository<Question>().GetAllWithSpecAsync(spec);

        return questions;
    }

    public async Task<Question?> UpdateQuestion(int questionId, Question updatedQuestion)
    {
        var question = await _unitOfWork.Repository<Question>().GetByIdAsync(questionId);

        if (question == null || updatedQuestion == null || string.IsNullOrWhiteSpace(updatedQuestion.Description))
            return null;

        question = updatedQuestion;

        try
        {
            _unitOfWork.Repository<Question>().Update(question);
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
