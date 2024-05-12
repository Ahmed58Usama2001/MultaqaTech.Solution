namespace MultaqaTech.Core.Specifications;

public class CourseWithFilterationForCountSpecifications : BaseSpecifications<Course>
{
    public CourseWithFilterationForCountSpecifications(CourseSpeceficationsParams speceficationsParams) 
          : base(e =>
           (
                 (string.IsNullOrEmpty(speceficationsParams.Language) || e.Language == speceficationsParams.Language) &&
                 (!speceficationsParams.InstractorId.HasValue || e.InstructorId == speceficationsParams.InstractorId) &&
                 (!speceficationsParams.StudentId.HasValue || e.EnrolledStudentsIds.Contains((int)speceficationsParams.StudentId)) &&
                 (!speceficationsParams.SubjectId.HasValue || e.SubjectId == speceficationsParams.SubjectId) &&
                 (!speceficationsParams.MinPrice.HasValue || e.Price >= speceficationsParams.MinPrice) &&
                 (!speceficationsParams.MinPrice.HasValue || e.Price <= speceficationsParams.MaxPrice) &&
                 (!speceficationsParams.CourseLevel.HasValue || e.Level == speceficationsParams.CourseLevel)
           ))
    {

    }
}

