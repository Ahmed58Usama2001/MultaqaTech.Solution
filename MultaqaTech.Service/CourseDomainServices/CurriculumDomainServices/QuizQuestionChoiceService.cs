namespace MultaqaTech.Service.CourseDomainServices.CurriculumDomainServices;

public class QuizQuestionChoiceService(IUnitOfWork unitOfWork) : IQuizQuestionChoiceService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<QuizQuestionChoice?> CreateQuizQuestionChoiceAsync(QuizQuestionChoice choice)
    {
        try
        {
            await _unitOfWork.Repository<QuizQuestionChoice>().AddAsync(choice);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return choice;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteQuizQuestionChoice(int choiceId)
    {
        var choice = await _unitOfWork.Repository<QuizQuestionChoice>().GetByIdAsync(choiceId);

        if (choice == null)
            return false;

        try
        {
            _unitOfWork.Repository<QuizQuestionChoice>().Delete(choice);

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

    public async Task<QuizQuestionChoice?> ReadByIdAsync(int choiceId)
    {
        var spec = new QuizQuestionChoiceWithIncludesSpecifications(choiceId);

        var choice = await _unitOfWork.Repository<QuizQuestionChoice>().GetByIdWithSpecAsync(spec);

        return choice;
    }

    public async Task<IReadOnlyList<QuizQuestionChoice>> ReadQuizQuestionChoicesAsync(QuizQuestionChoiceSpeceficationsParams speceficationsParams)
    {
        var spec = new QuizQuestionChoiceWithIncludesSpecifications(speceficationsParams);

        var choices = await _unitOfWork.Repository<QuizQuestionChoice>().GetAllWithSpecAsync(spec);

        return choices;
    }

    public async Task<QuizQuestionChoice?> UpdateQuizQuestionChoice(int choiceId, QuizQuestionChoice updatedChoice)
    {
        var choice = await _unitOfWork.Repository<QuizQuestionChoice>().GetByIdAsync(choiceId);

        if (choice == null || updatedChoice == null || string.IsNullOrWhiteSpace(updatedChoice.Content))
            return null;

        choice = updatedChoice;

        try
        {
            _unitOfWork.Repository<QuizQuestionChoice>().Update(choice);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return choice;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }
}
