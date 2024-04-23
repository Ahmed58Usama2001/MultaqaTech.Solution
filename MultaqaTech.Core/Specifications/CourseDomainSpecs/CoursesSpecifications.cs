namespace MultaqaTech.Core.Specifications;

public class CoursesSpecifications : BaseSpecifications<Course>
{
    public CoursesSpecifications(CourseSpeceficationsParams speceficationsParams)
        : base(e =>
           (
                 (string.IsNullOrEmpty(speceficationsParams.Language) || e.Language == speceficationsParams.Language) &&
                 (!speceficationsParams.InstractorId.HasValue) || e.InstractorId == speceficationsParams.InstractorId) &&
                 (!speceficationsParams.StudentId.HasValue) || e.EnrolledStudentsIds.Contains((int)speceficationsParams.StudentId) &&
                 (speceficationsParams.SubjectId == null || e.SubjectId == speceficationsParams.SubjectId) &&
                 (speceficationsParams.MinPrice == null || e.Price >= speceficationsParams.MinPrice) &&
                 (speceficationsParams.MaxPrice == null || e.Price <= speceficationsParams.MaxPrice) &&
                 (speceficationsParams.CourseLevel == null || e.Level == speceficationsParams.CourseLevel)
           )
    {
        AddIncludes();

        if (!string.IsNullOrEmpty(speceficationsParams.sort))
        {
            switch (speceficationsParams.sort)
            {
                case "RatingAsc":
                    AddOrderBy(p => p.Rating);
                    break;

                case "RatingDesc":
                    AddOrderByDesc(p => p.Rating);
                    break;

                case "PriceAsc":
                    AddOrderBy(p => p.Price);
                    break;

                case "PriceDesc":
                    AddOrderByDesc(p => p.Price);
                    break;

                case "DurationAsc":
                    AddOrderBy(p => p.Duration);
                    break;

                case "DurationDesc":
                    AddOrderByDesc(p => p.Duration);
                    break;

                case "TotalEnrolledAsc":
                    AddOrderBy(p => p.TotalEnrolled);
                    break;

                case "TotalEnrolledDesc":
                    AddOrderByDesc(p => p.TotalEnrolled);
                    break;

                case "UploadDateAsc":
                    AddOrderBy(p => p.UploadDate);
                    break;

                case "UploadDateDesc":
                    AddOrderByDesc(p => p.UploadDate);
                    break;

                case "LastUpdatedDateAsc":
                    AddOrderBy(p => p.LastUpdatedDate);
                    break;

                case "LastUpdatedDateDesc":
                    AddOrderByDesc(p => p.LastUpdatedDate);
                    break;

                default:
                    AddOrderByDesc(p => p.Rating);
                    break;
            }
        }
        else
            AddOrderByDesc(p => p.Rating);

        ApplyPagination((speceficationsParams.PageIndex - 1) * speceficationsParams.PageSize, speceficationsParams.PageSize);
    }

    public CoursesSpecifications(int id) : base(e => e.Id.Equals(id))
    {
        AddIncludes();
    }
    private void AddIncludes()
    {
        Includes.Add(c => c.Subject);
        //Includes.Add(c => c.Prerequisites);
        //Includes.Add(c => c.Tags);
    }
}