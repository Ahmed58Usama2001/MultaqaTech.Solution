namespace MultaqaTech.APIs.Controllers.CourseDomainControllers.CurriculumDomainControllers;

[Authorize]

public class CurriculumItemsController(
        IMapper mapper,
    ICurriculumItemService itemService, UserManager<AppUser> userManager, IUnitOfWork unitOfWork,
        ICurriculumSectionService sectionService,
MultaqaTechContext context) : BaseApiController
{
    private readonly ICurriculumItemService _itemService = itemService;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly MultaqaTechContext _context = context;
    private readonly ICurriculumSectionService _sectionService = sectionService;



    [HttpPut]
    [Route("{sectionId}/items/reorder")]
    public async Task<IActionResult> ReorderSections(int sectionId, [FromBody] List<int> newOrder)
    {
        CurriculumSection? existingSection = await _sectionService.ReadByIdAsync(sectionId);
        if (existingSection is null)
            return NotFound(new { Message = "Section wasn't Not Found", StatusCode = 404 });

        _context.Entry(existingSection).Reference(i => i.Course).Load();

        if (!await CheckIfRequestFromCreatorUser(existingSection.Course.InstructorId))
            return BadRequest(new ApiResponse(401));

        try
        {
            await _itemService.ReorderItems(sectionId, newOrder);
            return Ok(true);
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return BadRequest(false);
        }
    }

    [ProducesResponseType(typeof(ItemReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    [Route("{sectionId}/items")]
    public async Task<ActionResult<IReadOnlyList<ItemReturnDto>>> GetItemssBySectionId(int sectionId)
    {
        CurriculumItemSpeceficationsParams speceficationsParams = new CurriculumItemSpeceficationsParams { curriculumSectionId = sectionId };
        if (speceficationsParams.curriculumSectionId <= 0)
            return BadRequest(new { message = "Enter a suitable Section ID: It must be greater than 0" });

        CurriculumSection? section=await _sectionService.ReadByIdAsync(sectionId);
        if(section is null)
            return BadRequest(new { message = "Section Doesn't exist" });

        var items = await _itemService.ReadCurriculumItemsAsync(speceficationsParams);
        if (items == null)
            return NotFound(new ApiResponse(404));

        var mappedItems=_mapper.Map<IReadOnlyList<CurriculumItem>, IReadOnlyList<ItemReturnDto>>(items).OrderBy(i => i.Order);

        string? userEmail = User.FindFirstValue(ClaimTypes.Email);
        AppUser? storedUser = await _userManager.FindByEmailAsync(userEmail);
        Student? student = await _unitOfWork.Repository<Student>().FindAsync(S => S.AppUserId == storedUser.Id);
        if (student is null)
            return BadRequest(new ApiResponse(401));

        StudentCourseProgress? progress = null;
        StudentCourse? studentCourse = await _context.StudentCourses
                .FirstOrDefaultAsync(sc => sc.StudentId == student.Id && sc.CourseId== section.CourseId);

        if (studentCourse is null)
            return Ok(mappedItems);

        foreach (var item in mappedItems)
        {
            switch (item.ItemType)
            {
                case "Lecture":
                    progress = await _unitOfWork.Repository<StudentCourseProgress>().FindAsync(S => S.LectureId == item.Id &&
                    S.StudentCourseId == studentCourse.Id);
                    break;

                case "Quiz":
                    progress = await _unitOfWork.Repository<StudentCourseProgress>().FindAsync(S => S.QuizId == item.Id &&
                                S.StudentCourseId == studentCourse.Id);
                    break;
            }

            if(progress is not null)
            item.IsCompleted = progress.IsCompleted;
        }

        return Ok(mappedItems);
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
