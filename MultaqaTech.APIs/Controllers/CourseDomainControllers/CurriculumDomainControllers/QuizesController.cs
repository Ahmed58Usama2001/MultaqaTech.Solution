using MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

namespace MultaqaTech.APIs.Controllers.CourseDomainControllers.CurriculumDomainControllers;


public class QuizesController(
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

    [ProducesResponseType(typeof(QuizReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<QuizReturnDto>> CreateLectureAsync(QuizCreateDto quizDto)
    {
        if (quizDto is null) return BadRequest(new ApiResponse(400));


        CurriculumSection? existingSection = await _sectionService.ReadByIdAsync(quizDto.CurriculumSectionId);
        if (existingSection is null)
            return NotFound(new { Message = "Section wasn't Not Found", StatusCode = 404 });

        var mappedItem = _mapper.Map<QuizCreateDto, Quiz>(quizDto);
        mappedItem.CurriculumSection = existingSection;

        mappedItem.QuizQuestionPictureUrl = DocumentSetting.UploadFile(quizDto?.PictureUrl, $"Courses\\{existingSection.CourseId}\\Quizs\\QuizsImages");


        int suitableOrder = await GetSuitableOrderForNewItem(existingSection.Id);
        mappedItem.Order = suitableOrder;

        var createdQuiz = await _itemService.CreateCurriculumItemAsync(mappedItem);

        if (createdQuiz is null) return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<Quiz, QuizReturnDto>((Quiz)createdQuiz));
    }

    [ProducesResponseType(typeof(QuizReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<QuizReturnDto>> GetQuiz(int id)
    {
        Quiz? quiz = (Quiz?)await _itemService.ReadByIdAsync(id,CurriculumItemType.Quiz);

        if (quiz == null)
            return NotFound(new ApiResponse(404));


        return Ok(_mapper.Map<QuizReturnDto>(quiz));
    }

    [ProducesResponseType(typeof(QuizReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult<QuizReturnDto>> UpdateQuiz(int id, QuizUpdateDto updatedQuizDto)
    {
        Quiz? storedQuiz = (Quiz?)await _itemService.ReadByIdAsync(id, CurriculumItemType.Quiz);

        if (storedQuiz == null)
            return NotFound(new ApiResponse(404));

        CurriculumSection? existingSection = await _sectionService.ReadByIdAsync(storedQuiz.CurriculumSectionId);
        if (existingSection is null)
            return NotFound(new { Message = "Section wasn't Not Found", StatusCode = 404 });

        if (!string.IsNullOrEmpty(storedQuiz?.QuizQuestionPictureUrl))
            DocumentSetting.DeleteFile(storedQuiz.QuizQuestionPictureUrl);

        Quiz newQuiz = _mapper.Map<QuizUpdateDto, Quiz>(updatedQuizDto);
        newQuiz.Id = storedQuiz.Id;
        newQuiz.CurriculumSection = existingSection;
        newQuiz.CurriculumSectionId = existingSection.Id;


        if (updatedQuizDto.PictureUrl is not null)
            newQuiz.QuizQuestionPictureUrl = DocumentSetting.UploadFile(updatedQuizDto?.PictureUrl, $"Courses\\{existingSection.CourseId}\\Quizs\\QuizsImages");

        storedQuiz = (Quiz)await _itemService.UpdateCurriculumItem(storedQuiz, newQuiz);

        if (storedQuiz == null)
            return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<QuizReturnDto>(storedQuiz));
    }

    [ProducesResponseType(typeof(QuizReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSection(int id)
    {
        var quiz = await _unitOfWork.Repository<Quiz>().GetByIdAsync(id);
        if (quiz == null)
            return NotFound(new ApiResponse(404));

        var result = await _itemService.DeleteCurriculumItem(quiz);

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

        var existingLecs = await _itemService.ReadCurriculumItemsbyTypeAsync(speceficationsParams, CurriculumItemType.Lecture);
        var existingQuizes = await _itemService.ReadCurriculumItemsbyTypeAsync(speceficationsParams, CurriculumItemType.Quiz);
        List<CurriculumItem> existingItems = new List<CurriculumItem>();
        existingItems.AddRange(existingLecs);
        existingItems.AddRange(existingQuizes);

        int maxOrder = existingItems.Any() ? existingItems.Max(s => s.Order) : 0;

        return maxOrder + 1;
    }
}
