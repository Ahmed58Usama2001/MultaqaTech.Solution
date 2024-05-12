namespace MultaqaTech.Core.Specifications.CourseDomainSpecs.CurriculumEntitiesSpecs.CurriculumSectionSpecs;

public class NoteWithIncludesSpecifications : BaseSpecifications<Note>
{
    public NoteWithIncludesSpecifications(NoteSpeceficationsParams speceficationsParams)
        : base(e =>
           (
                 (speceficationsParams.writerStudentId == null || e.WriterStudentId == speceficationsParams.writerStudentId) &&
                 (speceficationsParams.lectureId == null || e.LectureId == speceficationsParams.lectureId) &&
            (string.IsNullOrEmpty(speceficationsParams.Search)
              || e.Content.ToLower().Contains(speceficationsParams.Search)

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

    public NoteWithIncludesSpecifications(int id)
        : base(p => p.Id == id)
    {
        //AddIncludes();
    }

    private void AddIncludes()
    {
    }

}
