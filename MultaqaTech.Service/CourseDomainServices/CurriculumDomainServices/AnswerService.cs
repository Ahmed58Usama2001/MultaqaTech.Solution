namespace MultaqaTech.Service.CourseDomainServices.CurriculumDomainServices;

public class AnswerService(IUnitOfWork unitOfWork) : IAnswerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Answer?> CreateAnswerAsync(Answer answer)
    {
        try
        {
            await _unitOfWork.Repository<Answer>().AddAsync(answer);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return answer;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteAnswer(int answerId)
    {
        var answer = await _unitOfWork.Repository<Answer>().GetByIdAsync(answerId);

        if (answer == null)
            return false;

        try
        {
            _unitOfWork.Repository<Answer>().Delete(answer);

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

    public async Task<IReadOnlyList<Answer>> ReadAnswersAsync(AnswerSpeceficationsParams speceficationsParams)
    {
        var spec = new AnswerWithIncludesSpecifications(speceficationsParams);

        var answers = await _unitOfWork.Repository<Answer>().GetAllWithSpecAsync(spec);

        return answers;
    }

    public async Task<Answer?> ReadByIdAsync(int answerId)
    {
        var spec = new AnswerWithIncludesSpecifications(answerId);

        var answer = await _unitOfWork.Repository<Answer>().GetByIdWithSpecAsync(spec);

        return answer;
    }

    public async Task<Answer?> UpdateAnswer(int answerId, Answer updatedAnswer)
    {
        var answer = await _unitOfWork.Repository<Answer>().GetByIdAsync(answerId);

        if (answer == null || updatedAnswer == null || string.IsNullOrWhiteSpace(updatedAnswer.Content))
            return null;

        answer = updatedAnswer;

        try
        {
            _unitOfWork.Repository<Answer>().Update(answer);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return answer;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }
}
