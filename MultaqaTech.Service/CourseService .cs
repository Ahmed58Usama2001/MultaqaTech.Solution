namespace MultaqaTech.Service;

public class CourseService(IUnitOfWork unitOfWork) : ICourseService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    private async Task BeforeCreate(Course course)
    {
        course.UploadDate = DateTime.Now;
        course.LastUpdatedDate = DateTime.Now;
        //
        //
        await Task.CompletedTask;
    }
    public async Task<Course?> CreateCourseAsync(Course course)
    {
        ArgumentNullException.ThrowIfNull(course);

        await BeforeCreate(course);

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
            Course? course = await _unitOfWork.Repository<Course>().GetByIdAsync(courseId);

            return course;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<IReadOnlyList<Course>?> ReadAllAsync()
    {
        try
        {
            IReadOnlyList<Course>? courses = await _unitOfWork.Repository<Course>().GetAllAsync();

            return courses;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    private async Task BeforeUpdate(Course course)
    {
        course.LastUpdatedDate = DateTime.Now;
        //
        //
        await Task.CompletedTask;
    }
    public async Task<Course?> UpdateCourse(Course course)
    {
        ArgumentNullException.ThrowIfNull(course);

        await BeforeUpdate(course);

        Course? courseFromDb = await _unitOfWork.Repository<Course>().GetByIdAsync(course.Id);

        if (courseFromDb is null) return null;

        courseFromDb = course;
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

    public async Task<bool> DeleteCourse(int subjectId)
    {
        Course? course = await _unitOfWork.Repository<Course>().GetByIdAsync(subjectId);

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
}