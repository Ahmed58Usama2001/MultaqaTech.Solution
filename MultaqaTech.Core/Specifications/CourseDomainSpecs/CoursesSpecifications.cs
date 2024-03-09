using MultaqaTech.Core.Entities.CourseDomainEntities;

namespace MultaqaTech.Core.Specifications;

public class CoursesSpecifications : BaseSpecifications<Course>
{
    public CoursesSpecifications(CourseSpeceficationsParams speceficationsParams)
        : base(e =>
            (
            (!string.IsNullOrEmpty(speceficationsParams.instractorId) || e.InstructorId == speceficationsParams.instractorId) &&
            (!string.IsNullOrEmpty(speceficationsParams.StudentId) || e.EnrolledStudentsIds.Contains(speceficationsParams.instractorId))
            ))
    {
        AddIncludes();
        ApplyPagination((speceficationsParams.PageIndex - 1) * speceficationsParams.PageSize, speceficationsParams.PageSize);
    }

    public CoursesSpecifications(int id) : base(e=>e.Id.Equals(id))
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