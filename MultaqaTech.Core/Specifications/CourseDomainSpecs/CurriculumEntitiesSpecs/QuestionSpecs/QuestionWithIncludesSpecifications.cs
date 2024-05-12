namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class QuestionWithIncludesSpecifications : BaseSpecifications<Question>
{
    public QuestionWithIncludesSpecifications(QuestionSpeceficationsParams speceficationsParams)
        : base(e =>
           (
                 (speceficationsParams.lectureId == null || e.LectureId == speceficationsParams.lectureId) &&
            (string.IsNullOrEmpty(speceficationsParams.Search)
              || e.Title.ToLower().Contains(speceficationsParams.Search)
              || e.Details.ToLower().Contains(speceficationsParams.Search)

           )))

    {
        //AddIncludes();

        if (!string.IsNullOrEmpty(speceficationsParams.sort))
        {
            switch (speceficationsParams.sort)
            {
                case "PublishingDateAsc":
                    AddOrderBy(p => p.PublishingDate);
                    break;

                case "PublishingDateDesc":
                    AddOrderByDesc(p => p.PublishingDate);
                    break;

                default:
                    AddOrderByDesc(p => p.PublishingDate);
                    break;
            }
        }
        else
            AddOrderByDesc(p => p.PublishingDate);

        ApplyPagination((speceficationsParams.PageIndex - 1) * speceficationsParams.PageSize, speceficationsParams.PageSize);
    }

    public QuestionWithIncludesSpecifications(int id)
        : base(p => p.Id == id)
    {
      //  AddIncludes();
    }

    private void AddIncludes()
    {
    }

}
