namespace MultaqaTech.APIs.Controllers.CourseDomainControllers.CurriculumDomainControllers;

[Authorize]
public class AnswersController(
    IMapper mapper,
    UserManager<AppUser> userManager,
    IAnswerService answerService,
    IQuestionService curriculumItemService,
    IUnitOfWork unitOfWork,
    MultaqaTechContext context) : BaseApiController
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAnswerService _answerService = answerService;
    private readonly IQuestionService _curriculumItemService = curriculumItemService;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly MultaqaTechContext _context = context;

    [ProducesResponseType(typeof(AnswerReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<AnswerReturnDto>> CreateAnswerAsync(AnswerCreateDto answerDto)
    {
        if (answerDto is null) return BadRequest(new ApiResponse(400));

        Question? existingQuestion = await _curriculumItemService.ReadByIdAsync(answerDto.QuestionId);
        if (existingQuestion is null)
            return NotFound(new ApiResponse(404, "Question wasn't Not Found"));

        var mappedAnswer = _mapper.Map<AnswerCreateDto, Answer>(answerDto);
        mappedAnswer.Question =existingQuestion;

        string? userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (userEmail is null) return NotFound(new ApiResponse(401));

        AppUser? storedUser = await _userManager.FindByEmailAsync(userEmail);
        if (storedUser is null) return NotFound(new ApiResponse(401));

        Student? student = await _unitOfWork.Repository<Student>().FindAsync(S => S.AppUserId == storedUser.Id);
        if (student is null)
            return NotFound(new ApiResponse(401));

        mappedAnswer.AnswererId = student.Id;

        var createdAnswer = await _answerService.CreateAnswerAsync(mappedAnswer);

        if (createdAnswer is null) return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<Answer, AnswerReturnDto>(createdAnswer));
    }

    [ProducesResponseType(typeof(AnswerReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    [Route("{QuestionId}/answers")]
    public async Task<ActionResult<IReadOnlyList<AnswerReturnDto>>> GetAnswersByQuestionId(int QuestionId)
    {
        AnswerSpeceficationsParams speceficationsParams = new AnswerSpeceficationsParams { questionId = QuestionId };
        if (speceficationsParams.questionId <= 0)
            return BadRequest(new ApiResponse(404, "Enter a suitable Question ID: It must be greater than 0"));

        var answers = await _answerService.ReadAnswersAsync(speceficationsParams);
        if (answers == null)
            return NotFound(new ApiResponse(404));

        var count = await _answerService.GetCountAsync(speceficationsParams);

        var data = _mapper.Map<IReadOnlyList<Answer>, IReadOnlyList<AnswerReturnDto>>(answers);

        foreach (var item in data)
        {
            Student? student = await _unitOfWork.Repository<Student>().FindAsync(S => S.Id == item.AnswererId);
            _context?.Entry(student).Reference(c => c.AppUser).Load();
            item.AnswererName = student?.AppUser?.UserName ?? " ";
        }

        return Ok(new Pagination<AnswerReturnDto>(speceficationsParams.PageIndex, speceficationsParams.PageSize, count, data));
    }

    [ProducesResponseType(typeof(AnswerReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult<AnswerReturnDto>> UpdateAnswer(int id, AnswerUpdateDto updatedAnswerDto)
    {
        Answer? storedAnswer = await _answerService.ReadByIdAsync(id);

        if (storedAnswer == null)
            return NotFound(new ApiResponse(404));

        if (!await CheckIfRequestFromCreatorUser(storedAnswer.AnswererId))
            return BadRequest(new ApiResponse(401));


        Question? existingQuestion = await _curriculumItemService.ReadByIdAsync(storedAnswer.QuestionId);
        if (existingQuestion is null)
            return NotFound(new ApiResponse(404, "Question wasn't Not Found"));

        Answer newAnswer = _mapper.Map<AnswerUpdateDto, Answer>(updatedAnswerDto);
        newAnswer.Id = storedAnswer.Id;
        newAnswer.AnswererId = storedAnswer.AnswererId;
        newAnswer.Question = existingQuestion;
        newAnswer.QuestionId = existingQuestion.Id;

        storedAnswer = await _answerService.UpdateAnswer(storedAnswer, newAnswer);

        if (storedAnswer == null)
            return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<AnswerReturnDto>(storedAnswer));
    }

    [ProducesResponseType(typeof(AnswerReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnswer(int id)
    {
        Answer? answer = await _answerService.ReadByIdAsync(id);

        if (answer == null)
            return NotFound(new ApiResponse(404));

        if (!await CheckIfRequestFromCreatorUser(answer.AnswererId))
            return BadRequest(new ApiResponse(401));

        var result = await _answerService.DeleteAnswer(id);

        if (result)
            return Ok(true);

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

