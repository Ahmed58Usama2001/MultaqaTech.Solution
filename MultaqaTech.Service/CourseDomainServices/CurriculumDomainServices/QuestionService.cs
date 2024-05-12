using MultaqaTech.Core.Entities.BlogPostDomainEntities;

namespace MultaqaTech.Service.CourseDomainServices.CurriculumDomainServices;

public class QuestionService(IUnitOfWork unitOfWork) : IQuestionService
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

    public async Task<bool> DeleteQuestion(Question question)
    {
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

    public async Task<int> GetCountAsync(QuestionSpeceficationsParams speceficationsParams)
    {
        var countSpec = new QuestionWithFilterationForCountSpecifications(speceficationsParams);

        var count = await _unitOfWork.Repository<Question>().GetCountAsync(countSpec);

        return count;
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

    public async Task<Question?> UpdateQuestion(Question storedQuestion, Question newQuestion)
    {
        if (newQuestion == null || storedQuestion == null)
            return null;

        storedQuestion.Title = newQuestion.Title;
        storedQuestion.Details = newQuestion.Details;
        storedQuestion.QuestionPictureUrl = newQuestion.QuestionPictureUrl;

        try
        {
            _unitOfWork.Repository<Question>().Update(storedQuestion);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return storedQuestion;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }
}
