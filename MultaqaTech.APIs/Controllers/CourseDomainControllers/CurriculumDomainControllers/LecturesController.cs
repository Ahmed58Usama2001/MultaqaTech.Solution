namespace MultaqaTech.APIs.Controllers.CourseDomainControllers.CurriculumDomainControllers;
[Authorize]

public class LecturesController(
    IMapper mapper,
    UserManager<AppUser> userManager,
    ICurriculumItemService itemService,
    ICurriculumSectionService sectionService,
    IUnitOfWork unitOfWork, MultaqaTechContext context) : BaseApiController
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICurriculumItemService _itemService = itemService;
    private readonly ICurriculumSectionService _sectionService = sectionService;
    private readonly MultaqaTechContext _context = context;
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

        _context.Entry(existingSection).Reference(i => i.Course).Load();

        if (!await CheckIfRequestFromCreatorUser(existingSection.Course.InstructorId))
            return BadRequest(new ApiResponse(401));

        var mappedItem = _mapper.Map<LectureCreateDto, Lecture>(lectureDto);
        mappedItem.CurriculumSection = existingSection;

        mappedItem.VideoUrl = DocumentSetting.UploadFile(lectureDto?.VideoUrl, $"Courses\\{existingSection.CourseId}\\Lectures\\LecturesVideos");


        int suitableOrder = await GetSuitableOrderForNewItem(existingSection.Id);
        mappedItem.Order = suitableOrder;

        mappedItem.CurriculumItemType = CurriculumItemType.Lecture;

        var createdLecture = await _itemService.CreateCurriculumItemAsync(mappedItem);

        if (createdLecture is null) return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<Lecture, LectureReturnDto>((Lecture)createdLecture));
    }

    [ProducesResponseType(typeof(LectureReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<LectureReturnDto>> GetLecture(int id)
    {
        Lecture? lecture = (Lecture?)await _itemService.ReadByIdAsync(id,CurriculumItemType.Lecture);
        if (lecture == null)
            return NotFound(new ApiResponse(404));

        _context.Entry(lecture).Reference(i => i.CurriculumSection).Load();
        _context.Entry(lecture.CurriculumSection).Reference(i => i.Course).Load();

        string? userEmail = User.FindFirstValue(ClaimTypes.Email);
        AppUser? storedUser = await _userManager.FindByEmailAsync(userEmail);
        Student? student = await _unitOfWork.Repository<Student>().FindAsync(S => S.AppUserId == storedUser.Id);
        if (student is null)
            return BadRequest(new ApiResponse(401));

        if (!await CheckIfRequestFromCreatorUser(lecture.CurriculumSection.Course.InstructorId)&&!lecture.CurriculumSection.Course.EnrolledStudentsIds.Contains(student.Id))
            return BadRequest(new ApiResponse(401));

        if (!await CheckIfRequestFromCreatorUser(lecture.CurriculumSection.Course.InstructorId))
        {
            StudentCourse studentCourse = await _unitOfWork.Repository<StudentCourse>().FindAsync(SC => SC.StudentId == student.Id &&
              SC.CourseId == lecture.CurriculumSection.CourseId);

            var updated = await _itemService.UpdateCurriculumItemCompletionStateInStudentProgress(id, studentCourse.Id, CurriculumItemType.Lecture);
            if (!updated)
                return BadRequest(new ApiResponse(400));
        }

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

        _context.Entry(existingSection).Reference(i => i.Course).Load();

        if (!await CheckIfRequestFromCreatorUser(existingSection.Course.InstructorId))
            return BadRequest(new ApiResponse(401));


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
        var lecture = await _itemService.ReadByIdAsync(id,CurriculumItemType.Lecture);
        if (lecture == null)
            return NotFound(new ApiResponse(404));

        _context.Entry(lecture).Reference(i => i.CurriculumSection).Load();
        _context.Entry(lecture.CurriculumSection).Reference(i => i.Course).Load();


        if (!await CheckIfRequestFromCreatorUser(lecture.CurriculumSection.Course.InstructorId))
            return BadRequest(new ApiResponse(401));

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

