namespace MultaqaTech.APIs.Controllers.CourseDomainControllers.CurriculumDomainControllers;

[Authorize]
public class QuestionsController(
    IMapper mapper,
    UserManager<AppUser> userManager,
    IQuestionService questionService,
    ICurriculumItemService curriculumItemService,
    IUnitOfWork unitOfWork,
    MultaqaTechContext context) : BaseApiController
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IQuestionService _questionService = questionService;
    private readonly ICurriculumItemService _curriculumItemService = curriculumItemService;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly MultaqaTechContext _context = context;

    [ProducesResponseType(typeof(QuestionReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<QuestionReturnDto>> CreateQuestionAsync(QuestionCreateDto questionDto)
    {
        if (questionDto is null) return BadRequest(new ApiResponse(400));

        CurriculumItem? existingLecture = await _curriculumItemService.ReadByIdAsync(questionDto.LectureId, CurriculumItemType.Lecture);
        if (existingLecture is null)
            return NotFound(new ApiResponse(404, "Lecture wasn't Not Found"));

        var mappedQuestion = _mapper.Map<QuestionCreateDto, Question>(questionDto);
        mappedQuestion.Lecture = (Lecture)existingLecture;

        if(questionDto.QuestionPicture is not null)
        mappedQuestion.QuestionPictureUrl = DocumentSetting.UploadFile(questionDto?.QuestionPicture, $"Questions\\{existingLecture.Id}\\QuestionsImages");

        string? userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (userEmail is null) return NotFound(new ApiResponse(401));

        AppUser? storedUser = await _userManager.FindByEmailAsync(userEmail);
        if (storedUser is null) return NotFound(new ApiResponse(401));

        Student? student = await _unitOfWork.Repository<Student>().FindAsync(S => S.AppUserId == storedUser.Id);
        if (student is null)
            return NotFound(new ApiResponse(401));

        mappedQuestion.AskerId = student.Id;

        var createdQuestion = await _questionService.CreateQuestionAsync(mappedQuestion);

        if (createdQuestion is null) return BadRequest(new ApiResponse(400));

        var returnedQuestion = _mapper.Map<Question, QuestionReturnDto>(createdQuestion);
        returnedQuestion.AskerName = storedUser?.UserName ?? " ";

        return Ok(returnedQuestion);
    }

    [ProducesResponseType(typeof(QuestionReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    [Route("{LectureId}/questions")]
    public async Task<ActionResult<IReadOnlyList<QuestionReturnDto>>> GetQuestionsByLectureId(int LectureId)
    {
        QuestionSpeceficationsParams speceficationsParams = new QuestionSpeceficationsParams { lectureId = LectureId };
        if (speceficationsParams.lectureId <= 0)
            return BadRequest(new ApiResponse(404, "Enter a suitable Lecture ID: It must be greater than 0"));

        var questions = await _questionService.ReadQuestionsAsync(speceficationsParams);
        if (questions == null)
            return NotFound(new ApiResponse(404));

        var count = await _questionService.GetCountAsync(speceficationsParams);

        var data = _mapper.Map<IReadOnlyList<Question>, IReadOnlyList<QuestionReturnDto>>(questions);

        foreach (var item in data)
        {
            Student? student = await _unitOfWork.Repository<Student>().FindAsync(S => S.Id == item.AskerId);
            _context?.Entry(student).Reference(c => c.AppUser).Load();
            item.AskerName=student?.AppUser?.UserName??" ";
        }

        return Ok(new Pagination<QuestionReturnDto>(speceficationsParams.PageIndex, speceficationsParams.PageSize, count, data));
    }

    [ProducesResponseType(typeof(QuestionReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<QuestionReturnDto>> GetQuestion(int id)
    {
        Question? question = await _questionService.ReadByIdAsync(id);

        if (question == null)
            return NotFound(new ApiResponse(404));

        var returnedQuestion = _mapper.Map<QuestionReturnDto>(question);

        Student? student = await _unitOfWork.Repository<Student>().FindAsync(S => S.Id == returnedQuestion.AskerId);
        _context?.Entry(student).Reference(c => c.AppUser).Load();
        returnedQuestion.AskerName = student?.AppUser?.UserName ?? " ";

        return Ok(returnedQuestion);
    }

    [ProducesResponseType(typeof(QuestionReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult<QuestionReturnDto>> UpdateQuestion(int id, QuestionUpdateDto updatedQuestionDto)
    {
        Question? storedQuestion = await _questionService.ReadByIdAsync(id);

        if (storedQuestion == null)
            return NotFound(new ApiResponse(404));

        if (!await CheckIfRequestFromCreatorUser(storedQuestion.AskerId))
            return BadRequest(new ApiResponse(401));

        if (!string.IsNullOrEmpty(storedQuestion?.QuestionPictureUrl))
            DocumentSetting.DeleteFile(storedQuestion.QuestionPictureUrl);

        CurriculumItem? existingLecture = await _curriculumItemService.ReadByIdAsync(storedQuestion.LectureId, CurriculumItemType.Lecture);
        if (existingLecture is null)
            return NotFound(new ApiResponse(404, "Lecture wasn't Not Found"));

        Question newQuestion = _mapper.Map<QuestionUpdateDto, Question>(updatedQuestionDto);
        newQuestion.Id = storedQuestion.Id;
        newQuestion.Lecture = (Lecture)existingLecture;
        newQuestion.LectureId = existingLecture.Id;

        if (updatedQuestionDto.QuestionPicture is not null)
            newQuestion.QuestionPictureUrl = DocumentSetting.UploadFile(updatedQuestionDto?.QuestionPicture,
                $"Questions\\{existingLecture.Id}\\QuestionsImages");

        storedQuestion = await _questionService.UpdateQuestion(storedQuestion, newQuestion);

        if (storedQuestion == null)
            return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<QuestionReturnDto>(storedQuestion));
    }

    [ProducesResponseType(typeof(QuestionReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuestion(int id)
    {
        Question? question =  await _questionService.ReadByIdAsync(id);

        if (question == null)
            return NotFound(new ApiResponse(404));


        if (!await CheckIfRequestFromCreatorUser(question.AskerId))
            return BadRequest(new ApiResponse(401));

        var result = await _questionService.DeleteQuestion(question);

        if (result)
        {
            if (!string.IsNullOrEmpty(question.QuestionPictureUrl))
                DocumentSetting.DeleteFile(question.QuestionPictureUrl);

            return Ok(true);
        }

        return BadRequest(new ApiResponse(400));
    }

    private async Task<bool> CheckIfRequestFromCreatorUser(int askerId)
    {
        string? userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (userEmail is null) return false;

        AppUser? storedUser = await _userManager.FindByEmailAsync(userEmail);
        if (storedUser is null) return false;

        Student? student = await _unitOfWork.Repository<Student>().FindAsync(S => S.AppUserId == storedUser.Id);
        if (student is null)
            return false;

        if (student.Id != askerId)
            return false;

        return true;
    }

}
