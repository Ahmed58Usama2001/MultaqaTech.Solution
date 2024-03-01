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

        var createdSubject = await _subjectService.CreateSubjectAsync(_mapper.Map<Subject>(subjectDto));

        if (createdSubject is null) return BadRequest(new ApiResponse(400));

        return Ok(createdSubject);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Subject>>> GetAllSubjects()
    {
        var subjects = await _subjectService.ReadAllAsync();
        return Ok(subjects);
    }

    [ProducesResponseType(typeof(SubjectDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<SubjectDto>> GetSubjectById(int id)
    {
        var subject = await _subjectService.ReadByIdAsync(id);

        if (subject == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok(_mapper.Map<Subject, SubjectDto>(subject));
    }

    [ProducesResponseType(typeof(Subject), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpPut]
    public async Task<ActionResult<Subject>> UpdateSubject(Subject updatedSubject)
    {
        var subject = await _subjectService.UpdateSubject(updatedSubject.Id, updatedSubject);

        if (subject == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok(_mapper.Map<Subject, SubjectDto>(subject));
    }

    [ProducesResponseType(typeof(Subject), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete]
    public async Task<ActionResult<Subject>> DeleteSubject(int subjectId)
    {
        var result = await _subjectService.DeleteSubject(subjectId);

        if (!result)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok();
    }

}
