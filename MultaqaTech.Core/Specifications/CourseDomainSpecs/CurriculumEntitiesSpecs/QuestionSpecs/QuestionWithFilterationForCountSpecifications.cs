namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class QuestionWithFilterationForCountSpecifications : BaseSpecifications<Question>
{
    public QuestionWithFilterationForCountSpecifications(QuestionSpeceficationsParams speceficationsParams) :
         base(e =>
           (
                 (speceficationsParams.askerId == null || e.AskerId == speceficationsParams.askerId) &&                
                 (speceficationsParams.lectureId == null || e.LectureId == speceficationsParams.lectureId)                 
           ))
    {

    }
}

