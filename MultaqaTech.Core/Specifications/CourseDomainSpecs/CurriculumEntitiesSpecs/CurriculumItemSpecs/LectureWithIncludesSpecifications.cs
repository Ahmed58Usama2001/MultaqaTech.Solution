namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class LectureWithIncludesSpecifications : BaseSpecifications<Lecture>
{
    public LectureWithIncludesSpecifications(CurriculumItemSpeceficationsParams speceficationsParams)
        : base(p =>
              (!speceficationsParams.curriculumSectionId.HasValue || p.CurriculumSectionId == speceficationsParams.curriculumSectionId.Value))

    {
        AddIncludes();
    }

    public LectureWithIncludesSpecifications(int id)
        : base(p => p.Id == id)
    {
        AddIncludes();
    }

    private void AddIncludes()
    {
        //Includes.Add(p => p.Notes);  ##TODO: I want to get the notes of the logged in user only 
        Includes.Add(p => p.Questions);
    }

}
