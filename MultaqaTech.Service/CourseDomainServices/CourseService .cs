namespace MultaqaTech.Service.CourseDomainServices;

public partial class CourseService(IUnitOfWork unitOfWork, ISubjectService subjectService) : ICourseService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISubjectService _subjectService = subjectService;

    private async Task BeforeCreate(Course course, Instructor? instructor)
    {
      

        course.UploadDate = DateTime.Now;
        course.LastUpdatedDate = DateTime.Now;
        course.InstractorId = instructor.Id;
        course.Instractor = instructor;
        course.Subject = await _subjectService.ReadByIdAsync(course.SubjectId) ?? new();
        course.Tags = await MapSubjectsAsync(course.TagsIds ?? new());
        course.Prerequisites = await MapSubjectsAsync(course.PrerequisitesIds ?? new());
        //
        //

        await Task.CompletedTask;
    }
    public async Task<Course?> CreateCourseAsync(Course course, Instructor? instructor)
    {
        ArgumentNullException.ThrowIfNull(course);

        await BeforeCreate(course, instructor);

        try
        {
            await _unitOfWork.Repository<Course>().AddAsync(course);

            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return course;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<IEnumerable<Course>> ReadAllCourses()
    {
        try
        {
            IEnumerable<Course>? courses = await _unitOfWork.Repository<Course>().GetAllAsync();

            return courses;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return Enumerable.Empty<Course>();
        }
    }
    public async Task<Course?> ReadByIdAsync(int courseId)
    {
        try
        {
            Course? course = await _unitOfWork.Repository<Course>().GetByIdWithSpecAsync(new CoursesSpecifications(courseId));

            return course;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<IEnumerable<Course>?> ReadCoursesForInstructor(CourseSpeceficationsParams courseSpeceficationsParams)
    {
        try
        {
            return await ReadCoursesWithSpecifications(courseSpeceficationsParams);
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<IEnumerable<Course>?> ReadCoursesWithSpecifications(CourseSpeceficationsParams courseSpeceficationsParams)
    {
        try
        {
            return await _unitOfWork.Repository<Course>().GetAllWithSpecAsync(new CoursesSpecifications(courseSpeceficationsParams));
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return null;
        }
    }

    public async Task<IEnumerable<Course>?> ReadCoursesForStudent(string studentId, CourseSpeceficationsParams courseSpeceficationsParams)
    {
        try
        {
            return await ReadCoursesWithSpecifications(courseSpeceficationsParams);
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    //private async Task BeforeUpdate(Course course)
    //{
    //    course.LastUpdatedDate = DateTime.Now;
    //    //
    //    //
    //    await Task.CompletedTask;
    //}
    public async Task<Course?> UpdateCourse(Course course, int courseId)
    {
        ArgumentNullException.ThrowIfNull(course);

        //await BeforeUpdate(course);

        Course? courseFromDb = await _unitOfWork.Repository<Course>().GetByIdWithSpecAsync(new CoursesSpecifications(courseId));

        if (courseFromDb is null) return null;

        courseFromDb.Level = course.Level;
        courseFromDb.Title = course.Title;
        courseFromDb.Language = course.Language;
        courseFromDb.ThumbnailUrl = course.ThumbnailUrl;
        courseFromDb.SubjectId = course.SubjectId;
        courseFromDb.Price = course.Price;
        courseFromDb.LearningObjectives = course.LearningObjectives;
        courseFromDb.Subject = await _subjectService.ReadByIdAsync(course.SubjectId) ?? new();
        courseFromDb.Tags = await MapSubjectsAsync(course.TagsIds ?? []);
        courseFromDb.Prerequisites = await MapSubjectsAsync(course.PrerequisitesIds ?? []);
        courseFromDb.LastUpdatedDate = DateTime.Now;

        try
        {
            _unitOfWork.Repository<Course>().Update(courseFromDb);

            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return courseFromDb;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteCourse(int courseId)
    {
        Course? course = await _unitOfWork.Repository<Course>().GetByIdAsync(courseId);

        if (course is null) return false;

        try
        {
            _unitOfWork.Repository<Course>().Delete(course);

            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0) return false;

            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return false;
        }
    }

    private async Task<List<Subject>> MapSubjectsAsync(List<int> subjectIds)
    {
        if (!subjectIds.Any()) return [];

        List<Subject> subjects = [];

        IReadOnlyList<Subject>? subjectsFromDb = await _subjectService.ReadSubjectsByIds(subjectIds);

        subjects.AddRange(subjectsFromDb);

        return subjects;
    }

    public async Task<(bool isUnique, int courseIdWithSameTitle)> CheckTitleUniqueness(string title)
    {
        IEnumerable<Course>? courses = await ReadAllCourses();
        Course? course = courses.FirstOrDefault(e => e.Title.Equals(title));
        if (course is not null)
            return (false, course.Id);
        else
            return (true, -1);
    }
}