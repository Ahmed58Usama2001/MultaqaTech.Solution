namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class AnswerWithIncludesSpecifications : BaseSpecifications<Answer>
{
    public AnswerWithIncludesSpecifications(AnswerSpeceficationsParams speceficationsParams)
        : base(e =>
           (
                 (speceficationsParams.answererId == null || e.AnswererId == speceficationsParams.answererId) &&
                 (speceficationsParams.questionId == null || e.QuestionId == speceficationsParams.questionId)
           ))

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

    public AnswerWithIncludesSpecifications(int id)
        : base(p => p.Id == id)
    {
        //AddIncludes();
    }

    private void AddIncludes()
    {
    }

}
