namespace MultaqaTech.Service.CourseDomainServices;

public partial class CourseService(IUnitOfWork unitOfWork, ISubjectService subjectService) : ICourseService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISubjectService _subjectService = subjectService;

    private async Task BeforeCreate(Course course, Instructor? instructor)
    {
        course.UploadDate = DateTime.Now;
        course.LastUpdatedDate = DateTime.Now;
        course.InstructorId = instructor.Id;
        course.Instructor = instructor;
        course.Subject = await _subjectService.ReadByIdAsync(course.SubjectId) ?? new();
        course.Tags = await MapSubjectsAsync(course.TagsIds ?? []);
        course.Prerequisites = await MapSubjectsAsync(course.PrerequisitesIds ?? []);
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
            Log.Error(ex.ToString(),ex);
            return null;
        }
    }

    public async Task<IEnumerable<Course>> ReadAllCourses()
    {
        try
        {
            return await _unitOfWork.Repository<Course>().GetAllAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString(),ex);
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
            Log.Error(ex.ToString(),ex);
            return null;
        }
    }


    public async Task<IEnumerable<Course>?> ReadCoursesWithSpecifications(CourseSpecificationsParams courseSpeceficationsParams)
    {
        var spec = new CoursesSpecifications(courseSpeceficationsParams);

        try
        {
            var courses = await _unitOfWork.Repository<Course>().GetAllWithSpecAsync(spec);

            if (courses is null) return null;

            return courses;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString(),ex);
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
            Log.Error(ex.ToString(),ex);
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
            Log.Error(ex.ToString(),ex);
            return false;
        }
    }

    private async Task<List<Subject>> MapSubjectsAsync(List<int> subjectIds)
    {
        if (subjectIds.Count == 0) return [];

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

    public async Task<int> GetCountAsync(CourseSpecificationsParams speceficationsParams)
    {
        var countSpec = new CourseWithFilterationForCountSpecifications(speceficationsParams);

        var count = await _unitOfWork.Repository<Course>().GetCountAsync(countSpec);

        return count;
    }

    public async Task<IReadOnlyList<Course>?> ReadByPredicateWithIncludes(Expression<Func<Course, bool>> criteriaExpression)
        => await _unitOfWork.Repository<Course>().GetAllWithSpecAsync(new CoursesSpecifications(criteriaExpression));
}