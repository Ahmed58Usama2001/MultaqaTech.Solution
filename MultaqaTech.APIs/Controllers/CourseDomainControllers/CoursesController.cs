﻿using MultaqaTech.Core.Entities.CourseDomainEntities;
using MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

namespace MultaqaTech.APIs.Controllers.CourseDomainControllers;

public partial class CoursesController(ICourseService courseService, IMapper mapper, UserManager<AppUser> userManager,
    IUnitOfWork unitOfWork, MultaqaTechContext context) : BaseApiController
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly MultaqaTechContext _context = context;
    private readonly ICourseService _courseService = courseService;
    private readonly UserManager<AppUser> _userManager = userManager;

    [Authorize]

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

        Instructor? instructor = await _unitOfWork.Repository<Instructor>().FindAsync(I => I.AppUserId == storedUser.Id);
        if (instructor is null)
            return NotFound(new ApiResponse(401));

        (bool isTitleUnique, _) = await _courseService.CheckTitleUniqueness(courseDto.Title);
        if (!isTitleUnique) return BadRequest(new ApiResponse(400, "Course Title Should Be Unique"));

        Course mappedCourse = _mapper.Map<Course>(courseDto);
        mappedCourse.ThumbnailUrl = DocumentSetting.UploadFile(courseDto.Thumbnail, "CoursesThumbnails");

        Course? createdCourse = await _courseService.CreateCourseAsync(mappedCourse, instructor);

        if (createdCourse is null) return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<CourseToReturnDto>(createdCourse));
    }

    [ProducesResponseType(typeof(List<CourseToReturnDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseToReturnDto>>> GetAllCoursesFiltered([FromQuery] CourseSpecificationsParams courseSpecificationsParams)
    {
        IEnumerable<Course>? courses = await _courseService.ReadCoursesWithSpecifications(courseSpecificationsParams);

        if (courses is null)
            return NotFound(new ApiResponse(404));

        foreach (var course in courses)
        {
            _context.Entry(course).Reference(c => c.Instructor).Load();
            _context.Entry(course.Instructor).Reference(i => i.AppUser).Load();
        }

        string? userEmail = User.FindFirstValue(ClaimTypes.Email);
        AppUser? storedUser = await _userManager.FindByEmailAsync(userEmail);
        Student? student = await _unitOfWork.Repository<Student>().FindAsync(S => S.AppUserId == storedUser.Id);
        if (student is null)
            return BadRequest(new ApiResponse(401));

        
        CourseToReturnDto? mappedCourse=null;
        List<CourseToReturnDto> data=new();
        foreach (var course in courses)
        {
            mappedCourse = _mapper.Map<Course, CourseToReturnDto>(course);
            if (course.EnrolledStudentsIds.Contains(student.Id))
              mappedCourse.WasBoughtBySignedInUser = true;

            data.Add(mappedCourse);
        }

        var count = await _courseService.GetCountAsync(courseSpecificationsParams);


        return Ok(new Pagination<CourseToReturnDto>(courseSpecificationsParams.PageIndex, courseSpecificationsParams.PageSize, count, data));
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

        string? userEmail = User.FindFirstValue(ClaimTypes.Email);
        AppUser? storedUser = await _userManager.FindByEmailAsync(userEmail);
        Student? student = await _unitOfWork.Repository<Student>().FindAsync(S => S.AppUserId == storedUser.Id);
        if (student is null)
            return BadRequest(new ApiResponse(401));

        var mappedCourse = _mapper.Map<CourseToReturnDto>(course);

        if(course.EnrolledStudentsIds.Contains(student.Id))
            mappedCourse.WasBoughtBySignedInUser = true;

        return Ok(mappedCourse);
    }

    [ProducesResponseType(typeof(List<CourseToReturnDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("GetCoursesForInstructorByInstructorId/{instructorId}")]
    public async Task<ActionResult<IEnumerable<CourseToReturnDto>>> GetCoursesForInstructorByInstructorId(int instructorId, [FromQuery] CourseSpecificationsParams courseSpecificationsParams)
    {
        courseSpecificationsParams.InstructorId = instructorId;

        IEnumerable<Course>? courses = await _courseService.ReadCoursesWithSpecifications(courseSpecificationsParams);
        if (courses is null)
            return NotFound(new ApiResponse(404));

        foreach (var course in courses)
        {
            _context.Entry(course).Reference(c => c.Instructor).Load();
            _context.Entry(course.Instructor).Reference(i => i.AppUser).Load();
        }

        var count = await _courseService.GetCountAsync(courseSpecificationsParams);

        var data = _mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseToReturnDto>>((IReadOnlyList<Course>)courses);

        return Ok(new Pagination<CourseToReturnDto>(courseSpecificationsParams.PageIndex, courseSpecificationsParams.PageSize, count, data));
    }

    [ProducesResponseType(typeof(List<CourseToReturnDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("GetCoursesForStudentByStudentId/{studentId}")]
    public async Task<ActionResult<IEnumerable<CourseToReturnDto>>> GetCoursesForStudentByStudentId(int studentId, [FromQuery] CourseSpecificationsParams courseSpecificationsParams)
    {
        courseSpecificationsParams.StudentId = studentId;

        IEnumerable<Course>? courses = await _courseService.ReadCoursesWithSpecifications(courseSpecificationsParams);
        if (courses is null)
            return NotFound(new ApiResponse(404));

        foreach (var course in courses)
        {
            _context.Entry(course).Reference(c => c.Instructor).Load();
            _context.Entry(course.Instructor).Reference(i => i.AppUser).Load();
        }


        var count = await _courseService.GetCountAsync(courseSpecificationsParams);
        var data = _mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseToReturnDto>>((IReadOnlyList<Course>)courses);

        foreach (var mappedCourse in data)
            mappedCourse.WasBoughtBySignedInUser=true;

        return Ok(new Pagination<CourseToReturnDto>(courseSpecificationsParams.PageIndex, courseSpecificationsParams.PageSize, count, data));
    }

    [ProducesResponseType(typeof(List<CourseToReturnDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("GetActiveCoursesForStudentByStudentId/{studentId}")]
    public async Task<ActionResult<IEnumerable<CourseToReturnDto>>> GetActiveCoursesForStudentByStudentId(int studentId, [FromQuery] CourseSpecificationsParams courseSpecificationsParams)
    {
        courseSpecificationsParams.StudentId = studentId;

        IEnumerable<Course>? storedCourses = await _courseService.ReadCoursesWithSpecifications(courseSpecificationsParams);
        if (storedCourses is null)
            return NotFound(new ApiResponse(404));

        List<StudentCourse> studentCourses = (List<StudentCourse>)_context.StudentCourses.Where(sc => sc.StudentId == studentId &&
        sc.CompletionPercentage>0 && sc.CompletionPercentage < 100);
        
        foreach (var course in storedCourses)
        {
            _context.Entry(course).Reference(c => c.Instructor).Load();
            _context.Entry(course.Instructor).Reference(i => i.AppUser).Load();
        }


        var count = await _courseService.GetCountAsync(courseSpecificationsParams);
        var data = _mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseToReturnDto>>((IReadOnlyList<Course>)storedCourses);

        foreach (var mappedCourse in data)
            mappedCourse.WasBoughtBySignedInUser = true;

        return Ok(new Pagination<CourseToReturnDto>(courseSpecificationsParams.PageIndex, courseSpecificationsParams.PageSize, count, data));
    }

    [ProducesResponseType(typeof(List<CourseToReturnDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("GetCompleteCoursesForStudentByStudentId/{studentId}")]
    public async Task<ActionResult<IEnumerable<CourseToReturnDto>>> GetCompleteCoursesForStudentByStudentId(int studentId, [FromQuery] CourseSpecificationsParams courseSpecificationsParams)
    {
        courseSpecificationsParams.StudentId = studentId;

        IEnumerable<Course>? courses = await _courseService.ReadCoursesWithSpecifications(courseSpecificationsParams);
        if (courses is null)
            return NotFound(new ApiResponse(404));

        foreach (var course in courses)
        {
            _context.Entry(course).Reference(c => c.Instructor).Load();
            _context.Entry(course.Instructor).Reference(i => i.AppUser).Load();
        }


        var count = await _courseService.GetCountAsync(courseSpecificationsParams);
        var data = _mapper.Map<IReadOnlyList<Course>, IReadOnlyList<CourseToReturnDto>>((IReadOnlyList<Course>)courses);

        foreach (var mappedCourse in data)
            mappedCourse.WasBoughtBySignedInUser = true;

        return Ok(new Pagination<CourseToReturnDto>(courseSpecificationsParams.PageIndex, courseSpecificationsParams.PageSize, count, data));
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

    [Authorize]

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

    [Authorize]

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