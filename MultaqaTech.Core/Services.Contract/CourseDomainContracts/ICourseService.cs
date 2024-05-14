namespace MultaqaTech.Core.Services.Contract.CourseDomainContracts;

public interface ICourseService : ICourseReviewService
{
    Task<Course?> CreateCourseAsync(Course course,Instructor? instructor);

    Task<Course?> ReadByIdAsync(int courseId);

    Task<Course?> UpdateCourse(Course course, int courseId);

    Task<bool> DeleteCourse(int courseId);

    Task<IEnumerable<Course>?> ReadCoursesWithSpecifications(CourseSpeceficationsParams courseSpeceficationsParams);

    Task<IEnumerable<Course>?> ReadCoursesForStudent(string studentId, CourseSpeceficationsParams courseSpeceficationsParams);

    Task<IEnumerable<Course>?> ReadCoursesForInstructor(CourseSpeceficationsParams courseSpeceficationsParams);
    Task<(bool isUnique, int courseIdWithSameTitle)> CheckTitleUniqueness(string title);

    Task<int> GetCountAsync(CourseSpeceficationsParams speceficationsParams);
}