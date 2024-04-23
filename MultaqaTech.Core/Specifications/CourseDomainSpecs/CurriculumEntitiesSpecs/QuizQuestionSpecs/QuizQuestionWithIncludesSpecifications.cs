namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class QuizQuestionWithIncludesSpecifications : BaseSpecifications<QuizQuestion>
{
    public QuizQuestionWithIncludesSpecifications(QuizQuestionSpeceficationsParams speceficationsParams)
        : base(e =>
           (          
                 (speceficationsParams.quizId == null || e.QuizId == speceficationsParams.quizId)
           ))

    {
        AddIncludes();
    }

    public QuizQuestionWithIncludesSpecifications(int id)
        : base(p => p.Id == id)
    {
        AddIncludes();
    }

    private void AddIncludes()
    {
        Includes.Add(p => p.QuizQuestionChoices);
    }

}
