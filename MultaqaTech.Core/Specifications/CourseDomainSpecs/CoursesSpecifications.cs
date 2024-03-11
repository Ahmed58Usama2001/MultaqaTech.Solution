namespace MultaqaTech.Core.Specifications;

public class CoursesSpecifications : BaseSpecifications<Course>
{
    public CoursesSpecifications(CourseSpeceficationsParams speceficationsParams)
        : base(e =>
           (
                 (string.IsNullOrEmpty(speceficationsParams.InstractorId) || e.InstructorId == speceficationsParams.InstractorId) &&
                 (string.IsNullOrEmpty(speceficationsParams.Language) || e.InstructorId == speceficationsParams.Language) &&
                 (string.IsNullOrEmpty(speceficationsParams.StudentId) || e.EnrolledStudentsIds.Contains(speceficationsParams.StudentId)) &&
                 (speceficationsParams.SubjectId == null || e.SubjectId == speceficationsParams.SubjectId) &&
                 (speceficationsParams.MinPrice == null || e.Price >= speceficationsParams.MinPrice) &&
                 (speceficationsParams.MaxPrice == null || e.Price <= speceficationsParams.MaxPrice) &&
                 (speceficationsParams.CourseLevel == null || e.Level == speceficationsParams.CourseLevel)
           ))
    {
        AddIncludes();
        ApplyPagination((speceficationsParams.PageIndex - 1) * speceficationsParams.PageSize, speceficationsParams.PageSize);
    }

    public CoursesSpecifications(int id) : base(e => e.Id.Equals(id))
    {
        AddIncludes();
    }
    private void AddIncludes()
    {
        Includes.Add(p => p.Subject);
        Includes.Add(p => p.Prerequisites);
        Includes.Add(p => p.Tags);
    }
}