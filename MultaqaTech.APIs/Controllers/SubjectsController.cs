using MultaqaTech.Core.Entities;

namespace MultaqaTech.APIs.Controllers;

public class SubjectsController(ISubjectService subjectService, IMapper mapper) : BaseApiController
{
    private readonly ISubjectService _subjectService = subjectService;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    public async Task<ActionResult<SubjectDto>> CreateSubject(SubjectDto subjectDto)
    {
        if (subjectDto is null) return BadRequest(new ApiResponse(400));

        var createdSubject = await _subjectService.Create(_mapper.Map<Subject>(subjectDto));

        if (createdSubject is null) return BadRequest(new ApiResponse(500));

        var createdSubjectDto = _mapper.Map<SubjectDto>(createdSubject);
        return Ok(createdSubjectDto);
    }
}