namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class QuizQuestionChoiceWithIncludesSpecifications : BaseSpecifications<QuizQuestionChoice>
{
    public QuizQuestionChoiceWithIncludesSpecifications(QuizQuestionChoiceSpeceficationsParams speceficationsParams)
        : base(e =>
           (
                 (speceficationsParams.quizQuestionId == null || e.QuizQuestionId == speceficationsParams.quizQuestionId)
           ))

    {
        //AddIncludes();

      
    }

    public QuizQuestionChoiceWithIncludesSpecifications(int id)
        : base(p => p.Id == id)
    {
        //AddIncludes();
    }

    private void AddIncludes()
    {
    }

}
