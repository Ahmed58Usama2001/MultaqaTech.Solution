namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class QuizWithIncludesSpecifications : BaseSpecifications<Quiz>
{
    public QuizWithIncludesSpecifications(CurriculumItemSpeceficationsParams speceficationsParams)
        : base(p =>
              (!speceficationsParams.curriculumSectionId.HasValue || p.CurriculumSectionId == speceficationsParams.curriculumSectionId.Value))

    {
    }

    public QuizWithIncludesSpecifications(int id)
        : base(p => p.Id == id)
    {
        AddIncludes();
    }

    private void AddIncludes()
    {      
        Includes.Add(p => p.QuizQuestions);
    }

}
