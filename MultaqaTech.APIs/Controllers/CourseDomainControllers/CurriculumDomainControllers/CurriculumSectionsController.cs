namespace MultaqaTech.APIs.Controllers.CourseDomainControllers.CurriculumDomainControllers;

[Authorize]
public class CurriculumSectionsController(
    IMapper mapper,
    UserManager<AppUser> userManager,
    ICurriculumSectionService curriculumSectionService,
    ICourseService courseService,
    IUnitOfWork unitOfWork) : BaseApiController
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICurriculumSectionService _curriculumSectionService = curriculumSectionService;
    private readonly ICourseService _courseService = courseService;
    private readonly UserManager<AppUser> _userManager = userManager;

    [ProducesResponseType(typeof(CurriculumSectionReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<CurriculumSectionReturnDto>> CreateSectionAsync(CurriculumSectionCreateDto curriculumSectionDto)
    {
        if (curriculumSectionDto is null) return BadRequest(new ApiResponse(400));


        Course? existingCourse = await _courseService.ReadByIdAsync(curriculumSectionDto.CourseId);
        if (existingCourse is null)
            return NotFound(new { Message = "Course wasn't Not Found", StatusCode = 404 });

        var mappedSection = _mapper.Map<CurriculumSectionCreateDto, CurriculumSection>(curriculumSectionDto);
        mappedSection.Course=existingCourse;

        int suitableOrder = await GetSuitableOrderForNewSection(existingCourse.Id);
        mappedSection.Order= suitableOrder;

        var createdSection = await _curriculumSectionService.CreateCurriculumSectionAsync(mappedSection);

        if (createdSection is null) return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<CurriculumSection, CurriculumSectionReturnDto>(createdSection));
    }

    [HttpPut]
    [Route("{courseId}/sections/reorder")] 
    public async Task<IActionResult> ReorderSections(int courseId, [FromBody] List<int> newOrder)
    {
        try
        {
            await _curriculumSectionService.ReorderSections(courseId, newOrder);
            return Ok();
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest(new { message = "Error reordering sections." }); 
        }
    }

    [ProducesResponseType(typeof(CurriculumSectionReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    [Route("{courseId}/sections")]
    public async Task<ActionResult<IReadOnlyList<CurriculumSectionReturnDto>>> GetSectionsByCourseId(int courseId)
    {
        CurriculumSectionSpeceficationsParams speceficationsParams= new CurriculumSectionSpeceficationsParams { courseId = courseId };
        if (speceficationsParams.courseId <=0 )
            return BadRequest(new { message = "Enter a suitable course ID: It must be greater than 0" });

        var sections = await _curriculumSectionService.ReadCurriculumSectionsAsync(speceficationsParams);
        if (sections == null)
            return NotFound(new ApiResponse(404));

        return Ok(_mapper.Map<IReadOnlyList<CurriculumSection>, IReadOnlyList<CurriculumSectionReturnDto>>(sections));
    }

    [ProducesResponseType(typeof(CurriculumSectionReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CurriculumSectionReturnDto>> GetSection(int id)
    {
        CurriculumSection? section = await _curriculumSectionService.ReadByIdAsync(id);

        if (section == null)
            return NotFound(new ApiResponse(404));


        return Ok(_mapper.Map<CurriculumSectionReturnDto>(section));
    }

    [ProducesResponseType(typeof(CurriculumSectionReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult<CurriculumSectionReturnDto>> UpdateSection(int id, CurriculumSectionUpdateDto updatedSectionDto)
    {
        CurriculumSection? storedSection = await _curriculumSectionService.ReadByIdAsync(id);

        if (storedSection == null)
            return NotFound(new ApiResponse(404));

        CurriculumSection newSection = _mapper.Map<CurriculumSectionUpdateDto, CurriculumSection>(updatedSectionDto);
        newSection.Id = storedSection.Id;

        storedSection = await _curriculumSectionService.UpdateCurriculumSection(storedSection, newSection);

        if (storedSection == null)
            return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<CurriculumSectionReturnDto>(storedSection));
    }

    [ProducesResponseType(typeof(CurriculumSectionReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSection(int id)
    {
        var section = await _unitOfWork.Repository<CurriculumSection>().GetByIdAsync(id);
        if (section == null)
            return NotFound(new ApiResponse(404));

        var result = await _curriculumSectionService.DeleteCurriculumSection(section);

        if (result)   
            return Ok(true);     

        return BadRequest(new ApiResponse(400));
    }


    private async Task<int> GetSuitableOrderForNewSection(int courseId)
    {
        CurriculumSectionSpeceficationsParams speceficationsParams = new CurriculumSectionSpeceficationsParams
         {
            courseId = courseId
         };

        var existingSections = await _curriculumSectionService.ReadCurriculumSectionsAsync(speceficationsParams);
        int maxOrder = existingSections.Any() ? existingSections.Max(s => s.Order) : 0;

        return maxOrder + 1; 
    }
}
