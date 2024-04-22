namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class QuizQuestionWithFilterationForCountSpecifications : BaseSpecifications<QuizQuestion>
{
    public QuizQuestionWithFilterationForCountSpecifications(QuizQuestionSpeceficationsParams speceficationsParams) :
         base(e =>
           (
                 (speceficationsParams.quizId == null || e.QuizId == speceficationsParams.quizId)                 
           ))
    {

    }
}

