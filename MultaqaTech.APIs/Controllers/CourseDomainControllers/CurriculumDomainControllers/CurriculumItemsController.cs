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

        var items = await _itemService.ReadCurriculumItemsAsync(speceficationsParams);
        if (items == null)
            return NotFound(new ApiResponse(404));

        return Ok(_mapper.Map<IReadOnlyList<CurriculumItem>, IReadOnlyList<ItemReturnDto>>(items).OrderBy(i=>i.Order));
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
