namespace MultaqaTech.APIs.Controllers.QuizDomainControllers.CurriculumDomainControllers;
[Authorize]
public class QuizQuestionsController(
    IMapper mapper,
    IQuizQuestionService quizQuestionService,
    IQuizQuestionChoiceService quizQuestionChoiceService,
    ICurriculumItemService quizService) : BaseApiController
{
    private readonly IMapper _mapper = mapper;
    private readonly IQuizQuestionService _quizQuestionService = quizQuestionService;
    private readonly IQuizQuestionChoiceService _quizQuestionChoiceService = quizQuestionChoiceService;
    private readonly ICurriculumItemService _quizService = quizService;

    [ProducesResponseType(typeof(QuizQuestionReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<QuizQuestionReturnDto>> CreateQuizQuestionAsync(QuizQuestionCreateDto quizQuestionDto)
    {
        if (quizQuestionDto is null) return BadRequest(new ApiResponse(400));

        Quiz? existingQuiz = (Quiz?)await _quizService.ReadByIdAsync(quizQuestionDto.QuizId,CurriculumItemType.Quiz);
        if (existingQuiz is null)
            return NotFound(new { Message = "Quiz wasn't Not Found", StatusCode = 404 });


        var mappedQuestion = new QuizQuestion()
        {
            Content = quizQuestionDto.Content,
            QuizId = existingQuiz.Id,
            Quiz = existingQuiz
        };

        var createdQuestion = await _quizQuestionService.CreateQuizQuestionAsync(mappedQuestion);
        List<QuizQuestionChoice> choices = new();

        QuizQuestionChoice? mappedChoice;
        QuizQuestionChoice? createdChoice;
        foreach (var choice in quizQuestionDto.QuizQuestionChoices)
        {
            mappedChoice = new QuizQuestionChoice()
            {
                Content = choice.Content,
                IsRight = choice.IsRight,
                Clarification = choice.Clarification,
                QuizQuestion = createdQuestion,
                QuizQuestionId = createdQuestion.Id
            };

            createdChoice = await _quizQuestionChoiceService.CreateQuizQuestionChoiceAsync(mappedChoice);
            if (createdChoice is null) return BadRequest(new ApiResponse(400));

            choices.Add(createdChoice);
        }
        createdQuestion.QuizQuestionChoices = choices;

        createdQuestion = await _quizQuestionService.UpdateQuizQuestion(createdQuestion.Id, createdQuestion);

        if (createdQuestion is null || createdQuestion?.QuizQuestionChoices?.Count() < 2) return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<QuizQuestion, QuizQuestionReturnDto>(createdQuestion));
    }

    [ProducesResponseType(typeof(QuizQuestionReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    [Route("{quizId}/questions")]
    public async Task<ActionResult<IReadOnlyList<QuizQuestionReturnDto>>> GetQuestionsByQuizId(int quizId)
    {
        QuizQuestionSpeceficationsParams speceficationsParams = new QuizQuestionSpeceficationsParams { quizId = quizId };
        if (speceficationsParams.quizId <= 0)
            return BadRequest(new { message = "Enter a suitable quiz ID: It must be greater than 0" });

        var questions = await _quizQuestionService.ReadQuizQuestionsAsync(speceficationsParams);
        if (questions == null)
            return NotFound(new ApiResponse(404));

        return Ok(_mapper.Map<IReadOnlyList<QuizQuestion>, IReadOnlyList<QuizQuestionReturnDto>>(questions));
    }

    [ProducesResponseType(typeof(QuizQuestionReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<QuizQuestionReturnDto>> GetQuestion(int id)
    {
        QuizQuestion? question = await _quizQuestionService.ReadByIdAsync(id);

        if (question == null)
            return NotFound(new ApiResponse(404));


        return Ok(_mapper.Map<QuizQuestionReturnDto>(question));
    }

    [ProducesResponseType(typeof(QuizQuestionReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult<QuizQuestionReturnDto>> UpdateQuestion(int id, QuizQuestionUpdateDto updatedQuestionDto)
    {
        QuizQuestion? storedQuestion = await _quizQuestionService.ReadByIdAsync(id);

        if (storedQuestion == null)
            return NotFound(new ApiResponse(404));

        storedQuestion?.QuizQuestionChoices?.Clear();

        List<QuizQuestionChoice> choices = new List<QuizQuestionChoice>();

        QuizQuestionChoice? mappedChoice;
        QuizQuestionChoice? newChoice;

        foreach (var choice in updatedQuestionDto.QuizQuestionChoices)
        {
            mappedChoice = new QuizQuestionChoice()
            {
                Content = choice.Content,
                IsRight = choice.IsRight,
                Clarification = choice.Clarification,
                QuizQuestion = storedQuestion,
                QuizQuestionId=storedQuestion.Id
            };
            newChoice = await _quizQuestionChoiceService.CreateQuizQuestionChoiceAsync(mappedChoice);
            if (newChoice is null) return BadRequest(new ApiResponse(400));

            choices.Add(newChoice);
        }


        storedQuestion.QuizQuestionChoices = choices;


        storedQuestion = await _quizQuestionService.UpdateQuizQuestion(id, storedQuestion);

        if (storedQuestion == null || storedQuestion?.QuizQuestionChoices?.Count() < 2)
            return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<QuizQuestionReturnDto>(storedQuestion));
    }


       

    [ProducesResponseType(typeof(QuizQuestionReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuestion(int id)
    {
        var result = await _quizQuestionService.DeleteQuizQuestion(id);

        if (!result)
            return BadRequest(new ApiResponse(400));

        return Ok(true);
    }

    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("answer/{Id}")]
    public async Task<IActionResult> DeleteAnswer(int Id)
    {
        var result = await _quizQuestionChoiceService.DeleteQuizQuestionChoice(Id);

        if (!result)
            return BadRequest(new ApiResponse(400));

        return Ok(true);
    }

}
