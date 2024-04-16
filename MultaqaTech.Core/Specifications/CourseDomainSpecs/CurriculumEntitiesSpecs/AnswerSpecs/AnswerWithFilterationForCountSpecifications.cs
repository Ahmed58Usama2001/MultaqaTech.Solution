namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class AnswerWithFilterationForCountSpecifications : BaseSpecifications<Answer>
{
    public AnswerWithFilterationForCountSpecifications(AnswerSpeceficationsParams speceficationsParams) :
         base(e =>
           (
                 (string.IsNullOrEmpty(speceficationsParams.answererId) || e.AnswererId == speceficationsParams.answererId) &&                
                 (speceficationsParams.questionId == null || e.QuestionId == speceficationsParams.questionId)                 
           ))
    {

    }
}

