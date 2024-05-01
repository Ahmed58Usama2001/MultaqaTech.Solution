namespace MultaqaTech.APIs.Controllers.CourseDomainControllers.CurriculumDomainControllers;

public class LecturesController(
    IMapper mapper,
    UserManager<AppUser> userManager,
    ICurriculumItemService itemService,
    ICurriculumSectionService sectionService,
    IUnitOfWork unitOfWork) : BaseApiController
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICurriculumItemService _itemService = itemService;
    private readonly ICurriculumSectionService _sectionService = sectionService;
    private readonly UserManager<AppUser> _userManager = userManager;

    [ProducesResponseType(typeof(LectureReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<LectureReturnDto>> CreateLectureAsync(LectureCreateDto lectureDto)
    {
        if (lectureDto is null) return BadRequest(new ApiResponse(400));


        CurriculumSection? existingSection = await _sectionService.ReadByIdAsync(lectureDto.CurriculumSectionId);
        if (existingSection is null)
            return NotFound(new { Message = "Section wasn't Not Found", StatusCode = 404 });

        var mappedItem = _mapper.Map<LectureCreateDto, Lecture>(lectureDto);
        mappedItem.CurriculumSection = existingSection;

        mappedItem.VideoUrl = DocumentSetting.UploadFile(lectureDto?.VideoUrl, $"Courses\\{existingSection.CourseId}\\Lectures\\LecturesVideos");


        int suitableOrder = await GetSuitableOrderForNewItem(existingSection.Id);
        mappedItem.Order = suitableOrder;

        var createdLecture = await _itemService.CreateCurriculumItemAsync(mappedItem);

        if (createdLecture is null) return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<Lecture, LectureReturnDto>((Lecture)createdLecture));
    }

    [ProducesResponseType(typeof(LectureReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<LectureReturnDto>> GetSection(int id)
    {
        Lecture? lecture = (Lecture?)await _itemService.ReadByIdAsync(id,CurriculumItemType.Lecture);

        if (lecture == null)
            return NotFound(new ApiResponse(404));


        return Ok(_mapper.Map<LectureReturnDto>(lecture));
    }

    [ProducesResponseType(typeof(LectureReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult<LectureReturnDto>> UpdateLecture(int id, LectureUpdateDto updatedLectureDto)
    {
        Lecture? storedLecture = (Lecture?)await _itemService.ReadByIdAsync(id,CurriculumItemType.Lecture);

        if (storedLecture == null)
            return NotFound(new ApiResponse(404));

        CurriculumSection? existingSection = await _sectionService.ReadByIdAsync(storedLecture.CurriculumSectionId);
        if (existingSection is null)
            return NotFound(new { Message = "Section wasn't Not Found", StatusCode = 404 });

        if (!string.IsNullOrEmpty(storedLecture?.VideoUrl))
            DocumentSetting.DeleteFile(storedLecture.VideoUrl);

        Lecture newLecture = _mapper.Map<LectureUpdateDto, Lecture>(updatedLectureDto);
        newLecture.Id = storedLecture.Id;
        newLecture.CurriculumSection=existingSection;
        newLecture.CurriculumSectionId=existingSection.Id;

        if (updatedLectureDto.VideoUrl is not null)
            newLecture.VideoUrl = DocumentSetting.UploadFile(updatedLectureDto?.VideoUrl, $"Courses\\{existingSection.CourseId}\\Lectures\\LecturesVideos");

        storedLecture = (Lecture)await _itemService.UpdateCurriculumItem(storedLecture, newLecture);

        if (storedLecture == null)
            return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<LectureReturnDto>(storedLecture));
    }

    [ProducesResponseType(typeof(LectureReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSection(int id)
    {
        var lecture = await _unitOfWork.Repository<Lecture>().GetByIdAsync(id);
        if (lecture == null)
            return NotFound(new ApiResponse(404));

        var result = await _itemService.DeleteCurriculumItem(lecture);

        if (result)
            return Ok(true);

        return BadRequest(new ApiResponse(400));
    }


    private async Task<int> GetSuitableOrderForNewItem(int sectionId)
    {
        CurriculumItemSpeceficationsParams speceficationsParams = new CurriculumItemSpeceficationsParams
        {
            curriculumSectionId = sectionId
        };

        var existingLecs = await _itemService.ReadCurriculumItemsbyTypeAsync(speceficationsParams,CurriculumItemType.Lecture );
        var existingQuizes = await _itemService.ReadCurriculumItemsbyTypeAsync(speceficationsParams,CurriculumItemType.Quiz );
        List<CurriculumItem> existingItems = new List<CurriculumItem>();
        existingItems.AddRange(existingLecs);
        existingItems.AddRange(existingQuizes);

        int maxOrder = existingItems.Any() ? existingItems.Max(s => s.Order) : 0;

        return maxOrder + 1;
    }
}

