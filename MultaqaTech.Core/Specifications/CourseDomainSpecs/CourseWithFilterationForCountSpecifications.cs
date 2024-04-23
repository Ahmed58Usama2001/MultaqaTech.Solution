namespace MultaqaTech.Core.Specifications;

public class CourseWithFilterationForCountSpecifications : BaseSpecifications<Course>
{
    public CourseWithFilterationForCountSpecifications(CourseSpeceficationsParams speceficationsParams) 
          : base(e =>
           (
                 (string.IsNullOrEmpty(speceficationsParams.Language) || e.Language == speceficationsParams.Language) &&
                 (!speceficationsParams.InstractorId.HasValue) || e.InstractorId == speceficationsParams.InstractorId) &&
                 (!speceficationsParams.StudentId.HasValue) || e.EnrolledStudentsIds.Contains((int) speceficationsParams.StudentId) &&
                 (speceficationsParams.SubjectId == null || e.SubjectId == speceficationsParams.SubjectId) &&
                 (speceficationsParams.MinPrice == null || e.Price >= speceficationsParams.MinPrice) &&
                 (speceficationsParams.MaxPrice == null || e.Price <= speceficationsParams.MaxPrice) &&
                 (speceficationsParams.CourseLevel == null || e.Level == speceficationsParams.CourseLevel)
           )
    {

    }
}

