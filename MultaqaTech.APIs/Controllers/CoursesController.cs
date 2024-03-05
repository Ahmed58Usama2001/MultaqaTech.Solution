namespace MultaqaTech.APIs.Controllers;

public class CoursesController(ICourseService courseService, IMapper mapper) : BaseApiController
{
    private readonly ICourseService _courseService = courseService;
    private readonly IMapper _mapper = mapper;


    [ProducesResponseType(typeof(Course), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<Course>> CreateCourse(CourseDto courseDto)
    {
        if (courseDto is null) return BadRequest(new ApiResponse(400));

        Course? createdCourse = await _courseService.CreateCourseAsync(_mapper.Map<Course>(courseDto));

        if (createdCourse is null) return BadRequest(new ApiResponse(400));

        return Ok(createdCourse);
    }

    [ProducesResponseType(typeof(CourseToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CourseToReturnDto>> GetCourseByCourseId(int id)
    {
        Course? course = await _courseService.ReadByIdAsync(id);

        if (course is null)
            return NotFound(new ApiResponse(404));

        return Ok(_mapper.Map<Course, CourseToReturnDto>(course));
    }

    [ProducesResponseType(typeof(CourseToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpPut]
    public async Task<ActionResult<CourseToReturnDto>> UpdateCourse(CourseDto course)
    {
        Course? updatedCourse = await _courseService.UpdateCourse(_mapper.Map<CourseDto, Course>(course));

        if (updatedCourse is null)
            return NotFound(new ApiResponse(404));

        return Ok(_mapper.Map<Course, CourseToReturnDto>(updatedCourse));
    }

    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(int id)
    {
        bool result = await _courseService.DeleteCourse(id);

        if (!result)
            return NotFound(new ApiResponse(400));

        return NoContent();
    }
}