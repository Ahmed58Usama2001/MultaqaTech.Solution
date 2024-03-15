namespace MultaqaTech.Core.Services.Contract.CourseDomainContracts;

public interface ICourseService
{
    Task<Course?> CreateCourseAsync(Course course, AppUser? instructor);

    Task<Course?> ReadByIdAsync(int courseId);

    Task<Course?> UpdateCourse(Course course, int courseId);

    Task<bool> DeleteCourse(int courseId);

    Task<IEnumerable<Course>?> ReadCoursesWithSpecifications(CourseSpeceficationsParams courseSpeceficationsParams);

    Task<IEnumerable<Course>?> ReadCoursesForStudent(string studentId, CourseSpeceficationsParams courseSpeceficationsParams);

    Task<IEnumerable<Course>?> ReadCoursesForInstructor(CourseSpeceficationsParams courseSpeceficationsParams);
    Task<bool> CheckTitleUniqueness(string title);
}