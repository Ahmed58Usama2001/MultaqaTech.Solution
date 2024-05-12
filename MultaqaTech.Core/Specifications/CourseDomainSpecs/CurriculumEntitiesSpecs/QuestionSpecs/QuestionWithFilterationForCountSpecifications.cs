namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class QuestionWithFilterationForCountSpecifications : BaseSpecifications<Question>
{
    public QuestionWithFilterationForCountSpecifications(QuestionSpeceficationsParams speceficationsParams) :
         base(e =>
           (
                 (speceficationsParams.lectureId == null || e.LectureId == speceficationsParams.lectureId) &&
            (string.IsNullOrEmpty(speceficationsParams.Search)
              || e.Title.ToLower().Contains(speceficationsParams.Search)
              || e.Details.ToLower().Contains(speceficationsParams.Search)

           )))
    {

    }
}

