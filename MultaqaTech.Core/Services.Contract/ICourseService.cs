using MultaqaTech.Core.Entities.CourseDomainEntities;

namespace MultaqaTech.Core.Services.Contract;

public interface ICourseService
{
    Task<Course?> CreateCourseAsync(Course course);

    Task<IReadOnlyList<Course>> ReadAllAsync();

    Task<Course?> ReadByIdAsync(int courseId);

    Task<Course?> UpdateCourse(Course course);

    Task<bool> DeleteCourse(int courseId);
}