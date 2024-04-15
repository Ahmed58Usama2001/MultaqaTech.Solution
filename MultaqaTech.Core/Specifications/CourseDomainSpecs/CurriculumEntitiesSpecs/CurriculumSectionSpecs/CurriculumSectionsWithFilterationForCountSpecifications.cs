namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class CurriculumSectionWithFilterationForCountSpecifications : BaseSpecifications<CurriculumSection>
{
    public CurriculumSectionWithFilterationForCountSpecifications(CurriculumSectionSpeceficationsParams speceficationsParams) :
          base(p =>
          (!speceficationsParams.courseId.HasValue || p.CourseId == speceficationsParams.courseId.Value))
    {

    }
}

