namespace MultaqaTech.APIs.Controllers;

public class SubjectsController(ISubjectService subjectService, IMapper mapper) : BaseApiController
{
    private readonly ISubjectService _subjectService = subjectService;
    private readonly IMapper _mapper = mapper;


    [ProducesResponseType(typeof(Subject),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<Subject>> CreateSubject(SubjectDto subjectDto)
    {
        if (subjectDto is null) return BadRequest(new ApiResponse(400));

        var createdSubject = await _subjectService.Create(_mapper.Map<Subject>(subjectDto));

        if (createdSubject is null) return BadRequest(new ApiResponse(500));

        return Ok(createdSubject);
    }
}