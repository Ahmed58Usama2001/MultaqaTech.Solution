namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class QuizQuestionChoiceWithFilterationForCountSpecifications : BaseSpecifications<QuizQuestionChoice>
{
    public QuizQuestionChoiceWithFilterationForCountSpecifications(QuizQuestionChoiceSpeceficationsParams speceficationsParams) :
         base(e =>
           (
                 (speceficationsParams.quizQuestionId == null || e.QuizQuestionId == speceficationsParams.quizQuestionId)                 
           ))
    {

    }
}

