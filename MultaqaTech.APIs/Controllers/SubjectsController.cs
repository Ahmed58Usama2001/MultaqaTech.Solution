namespace MultaqaTech.APIs.Controllers;

public class SubjectsController(ISubjectService subjectService, IMapper mapper) : BaseApiController
{
    private readonly ISubjectService _subjectService = subjectService;
    private readonly IMapper _mapper = mapper;


    [ProducesResponseType(typeof(Subject), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<Subject>> CreateSubject(SubjectDto subjectDto)
    {
        if (subjectDto is null) return BadRequest(new ApiResponse(400));

        var createdSubject = await _subjectService.Create(_mapper.Map<Subject>(subjectDto));

        if (createdSubject is null) return BadRequest(new ApiResponse(500));

        return Ok(createdSubject);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subject>>> GetAllAsync()
    {
        var subjects = await _subjectService.ReadAllAsync();

        return Ok(subjects);
    }

    [ProducesResponseType(typeof(Subject),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<Subject>> GetById(int id)
    {
        Subject? subject = await _subjectService.ReadById(id);

        if (subject is null) return NotFound(new ApiResponse(404));

        return Ok(subject);
    }
}