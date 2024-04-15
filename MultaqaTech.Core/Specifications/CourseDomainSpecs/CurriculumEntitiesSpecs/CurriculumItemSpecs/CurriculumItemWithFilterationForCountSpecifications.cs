namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class CurriculumItemWithFilterationForCountSpecifications : BaseSpecifications<CurriculumItem>
{
    public CurriculumItemWithFilterationForCountSpecifications(CurriculumItemSpeceficationsParams speceficationsParams) :
          base(p =>
          (!speceficationsParams.curriculumSectionId.HasValue || p.CurriculumSectionId == speceficationsParams.curriculumSectionId.Value))
    {

    }
}

