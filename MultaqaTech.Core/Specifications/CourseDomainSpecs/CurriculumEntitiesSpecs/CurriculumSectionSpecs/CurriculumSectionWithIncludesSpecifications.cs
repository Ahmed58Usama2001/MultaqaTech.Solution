namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class CurriculumSectionWithIncludesSpecifications : BaseSpecifications<CurriculumSection>
{
    public CurriculumSectionWithIncludesSpecifications(CurriculumSectionSpeceficationsParams speceficationsParams)
        : base(p =>
              (!speceficationsParams.courseId.HasValue || p.CourseId == speceficationsParams.courseId.Value))

    {
        AddIncludes();
    
        AddOrderBy(p => p.Order);         
    }

    public CurriculumSectionWithIncludesSpecifications(int id)
        : base(p => p.Id == id)
    {
        AddIncludes();
    }

    private void AddIncludes()
    {
        Includes.Add(p => p.Lectures);
        Includes.Add(p => p.Quizes);
    }

}
