namespace MultaqaTech.Service;

public class CourseService(IUnitOfWork unitOfWork, ISubjectService subjectService) : ICourseService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISubjectService _subjectService = subjectService;

    private async Task BeforeCreate(Course course, AppUser? instructor)
    {
        course.UploadDate = DateTime.Now;
        course.LastUpdatedDate = DateTime.Now;
        course.InstructorId = instructor.Id;
        //course.Instructor = instructor;
        course.Subject = await _subjectService.ReadByIdAsync(course.SubjectId) ?? new();
        course.Tags = await MapTagsAsync(course.TagsIds ?? new());
        course.Prerequisites = await MapPrerequestAsync(course.PrerequisitesIds ?? new());
        //
        //
        await Task.CompletedTask;
    }
    public async Task<Course?> CreateCourseAsync(Course course, AppUser? instructor)
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
            IEnumerable<Course>? coursesForInstructor = (await _unitOfWork.Repository<Course>().GetAllWithSpecAsync(new CoursesSpecifications(courseSpeceficationsParams)));

            return coursesForInstructor;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<IEnumerable<Course>?> ReadCoursesForStudent(string studentId, CourseSpeceficationsParams courseSpeceficationsParams)
    {
        try
        {
            courseSpeceficationsParams.StudentId = studentId;
            var coursesForStudent = (await _unitOfWork.Repository<Course>().GetAllWithSpecAsync(new CoursesSpecifications(courseSpeceficationsParams)));

            return coursesForStudent;
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

        Course? courseFromDb = await _unitOfWork.Repository<Course>().GetByIdAsync(courseId);

        if (courseFromDb is null) return null;

        courseFromDb.CourseLevel = course.CourseLevel;
        courseFromDb.Title = course.Title;
        courseFromDb.Language = course.Language;
        courseFromDb.ThumbnailUrl = course.ThumbnailUrl;
        courseFromDb.SubjectId = course.SubjectId;
        courseFromDb.Price = course.Price;
        courseFromDb.LearningObjectives = course.LearningObjectives;
        courseFromDb.Subject = await _subjectService.ReadByIdAsync(course.SubjectId) ?? new();
        courseFromDb.Tags = await MapTagsAsync(course.TagsIds ?? new());
        courseFromDb.Prerequisites = await MapPrerequestAsync(course.PrerequisitesIds ?? new());
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

    private async Task<List<CourseTag>> MapTagsAsync(List<int> tagsIds)
    {
        if (!tagsIds.Any()) return new();

        var courseTags = new List<CourseTag>();

        var subjectsFromDb = await _subjectService.ReadSubjectsByIds(tagsIds);

        foreach (var subject in subjectsFromDb)
            courseTags.Add(new()
            {
                TagId = subject.Id,
                TagName = subject.Name,
            });

        return courseTags;
    }

    private async Task<List<CoursePrerequist>> MapPrerequestAsync(List<int> prerequistsIds)
    {
        if (!prerequistsIds.Any()) return new();

        var coursePrerequists = new List<CoursePrerequist>();

        var subjectsFromDb = await _subjectService.ReadSubjectsByIds(prerequistsIds);

        foreach (var subject in subjectsFromDb)
            coursePrerequists.Add(new()
            {
                PrerequistId = subject.Id,
                PrerequistName = subject.Name,
            });

        return coursePrerequists;
    }
}