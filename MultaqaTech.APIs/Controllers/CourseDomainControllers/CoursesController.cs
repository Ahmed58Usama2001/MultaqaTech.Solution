namespace MultaqaTech.APIs.Controllers.CourseDomainControllers;

[Authorize]
public partial class CoursesController(ICourseService courseService, IMapper mapper, UserManager<AppUser> userManager,
    IUnitOfWork unitOfWork, MultaqaTechContext context) : BaseApiController
{
    private readonly ICourseService _courseService = courseService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly MultaqaTechContext _context = context;

    [ProducesResponseType(typeof(CourseToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    [HttpPost]
    public async Task<ActionResult<CourseToReturnDto>> CreateCourse(CourseDto courseDto)
    {
        if (courseDto is null) return BadRequest(new ApiResponse(400));

        string? instructorEmail = User.FindFirstValue(ClaimTypes.Email);
        if (instructorEmail is null) return NotFound(new ApiResponse(401));

        AppUser? storedUser = await _userManager.FindByEmailAsync(instructorEmail);
        if (storedUser is null) return NotFound(new ApiResponse(401));

        Instructor? instructor =await _unitOfWork.Repository<Instructor>().FindAsync(I=>I.AppUserId==storedUser.Id);
        if (instructor is null)
           return NotFound(new ApiResponse(401));

        (bool isTitleUnique, _) = await _courseService.CheckTitleUniqueness(courseDto.Title);
        if (!isTitleUnique) return BadRequest(new ApiResponse(400, "Course Title Should Be Unique"));

        Course? createdCourse = await _courseService.CreateCourseAsync(_mapper.Map<Course>(courseDto), instructor);

        if (createdCourse is null) return BadRequest(new ApiResponse(400));


        return Ok(_mapper.Map<CourseToReturnDto>(createdCourse));
    }

    [ProducesResponseType(typeof(List<CourseToReturnDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseToReturnDto>>> GetAllCoursesFiltered([FromQuery] CourseSpeceficationsParams courseSpeceficationsParams)
    {
        IEnumerable<Course>? courses = await _courseService.ReadCoursesWithSpecifications(courseSpeceficationsParams);

        if (courses is null)
            return NotFound(new ApiResponse(404));

        foreach (var course in courses)
        {
            _context.Entry(course).Reference(c => c.Instructor).Load();
            _context.Entry(course.Instructor).Reference(i => i.AppUser).Load();
        }

        var count = await _courseService.GetCountAsync(courseSpeceficationsParams);

        var data = _mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseToReturnDto>>((IReadOnlyList<Course>)courses);

        return Ok(new Pagination<BlogPostToReturnDto>(courseSpeceficationsParams.PageIndex, courseSpeceficationsParams.PageSize,
            count, (IReadOnlyList<BlogPostToReturnDto>)data));
    }

    [ProducesResponseType(typeof(CourseToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CourseToReturnDto>> GetCourseByCourseId(int id)
    {
        Course? course = await _courseService.ReadByIdAsync(id);

        if (course is null)
            return NotFound(new ApiResponse(404));

        _context.Entry(course).Reference(c => c.Instructor).Load();
        _context.Entry(course.Instructor).Reference(i => i.AppUser).Load();

        return Ok(_mapper.Map<CourseToReturnDto>(course));
    }

    [ProducesResponseType(typeof(List<CourseToReturnDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("GetCoursesForInstructorByInstructorId/{instructorId}")]
    public async Task<ActionResult<IEnumerable<CourseToReturnDto>>> GetCoursesForInstructorByInstructorId(int instructorId, [FromQuery] CourseSpeceficationsParams courseSpeceficationsParams)
    {
        courseSpeceficationsParams.InstractorId = instructorId;

        IEnumerable<Course>? courses = await _courseService.ReadCoursesForInstructor(courseSpeceficationsParams);

        if (courses is null)
            return NotFound(new ApiResponse(404));

        foreach (var course in courses)
        {
            _context.Entry(course).Reference(c => c.Instructor).Load();
            _context.Entry(course.Instructor).Reference(i => i.AppUser).Load();
        }

        var count = await _courseService.GetCountAsync(courseSpeceficationsParams);

        var data = _mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseToReturnDto>>((IReadOnlyList<Course>)courses);

        return Ok(new Pagination<BlogPostToReturnDto>(courseSpeceficationsParams.PageIndex, courseSpeceficationsParams.PageSize,
            count, (IReadOnlyList<BlogPostToReturnDto>)data));
    }

    [ProducesResponseType(typeof(List<CourseToReturnDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("GetCoursesForStudentByStudentId/{studentId}")]
    public async Task<ActionResult<IEnumerable<CourseToReturnDto>>> GetCoursesForStudentByStudentId(string studentId, [FromQuery] CourseSpeceficationsParams courseSpeceficationsParams)
    {
        IEnumerable<Course>? courses = await _courseService.ReadCoursesForStudent(studentId, courseSpeceficationsParams);

        if (courses is null)
            return NotFound(new ApiResponse(404));

        foreach (var course in courses)
        {
            _context.Entry(course).Reference(c => c.Instructor).Load();
            _context.Entry(course.Instructor).Reference(i => i.AppUser).Load();
        }

        var count = await _courseService.GetCountAsync(courseSpeceficationsParams);

        var data = _mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseToReturnDto>>((IReadOnlyList<Course>)courses);

        return Ok(new Pagination<BlogPostToReturnDto>(courseSpeceficationsParams.PageIndex, courseSpeceficationsParams.PageSize,
            count, (IReadOnlyList<BlogPostToReturnDto>)data));
    }

    [ProducesResponseType(typeof(InstructorReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("GetInstructors")]
    public async Task<ActionResult<IReadOnlyList<InstructorReturnDto>>> GetAllInstructors()
    {
        var instructors = await _unitOfWork.Repository<Instructor>().GetAllAsync();

        if (instructors == null)
            return NotFound(new ApiResponse(404));

        foreach (var instructor in instructors)
        {
            _context.Entry(instructor).Reference(i => i.AppUser).Load();  
        }

        return Ok(_mapper.Map<IReadOnlyList<Instructor>, IReadOnlyList<InstructorReturnDto>>(instructors));
    }

    [ProducesResponseType(typeof(CourseToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPut("{courseId}")]
    public async Task<ActionResult<CourseToReturnDto>> UpdateCourse(int courseId, CourseDto course)
    {
        Course? storedCourse = await _courseService.ReadByIdAsync(courseId);
        if (!await CheckIfRequestFromCreatorUser(storedCourse.InstructorId))
            return BadRequest(new ApiResponse(401));

        (bool isTitleUnique, int courseIdWithSameTitle) = await _courseService.CheckTitleUniqueness(course.Title);
        if (!isTitleUnique && courseId != courseIdWithSameTitle) return BadRequest(new ApiResponse(400, "Course Title Should Be Unique"));

        Course? updatedCourse = await _courseService.UpdateCourse(_mapper.Map<CourseDto, Course>(course), courseId);

        if (updatedCourse is null)
            return NotFound(new ApiResponse(404));

        return Ok(_mapper.Map<Course, CourseToReturnDto>(updatedCourse));
    }

    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(int id)
    {
        Course? storedCourse = await _courseService.ReadByIdAsync(id);
        if (!await CheckIfRequestFromCreatorUser(storedCourse.InstructorId))
            return BadRequest(new ApiResponse(401));

        bool result = await _courseService.DeleteCourse(id);

        if (!result)
            return NotFound(new ApiResponse(400));

        return NoContent();
    }

    private async Task<bool> CheckIfRequestFromCreatorUser(int instructorId)
    {
        string? userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (userEmail is null) return false;

        AppUser? storedUser = await _userManager.FindByEmailAsync(userEmail);
        if (storedUser is null) return false;

        Instructor? instructor = await _unitOfWork.Repository<Instructor>().FindAsync(S => S.AppUserId == storedUser.Id);
        if (instructor is null)
            return false;

        if (instructor.Id != instructorId)
            return false;

        return true;
    }
}