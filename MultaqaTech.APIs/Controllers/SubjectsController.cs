using MultaqaTech.APIs.Dtos.SettingsDtos;
using MultaqaTech.Core.Entities.SettingsEntities;

namespace MultaqaTech.APIs.Controllers;

public class SubjectsController(ISubjectService subjectService, IMapper mapper) : BaseApiController
{
    private readonly ISubjectService _subjectService = subjectService;
    private readonly IMapper _mapper = mapper;


    [ProducesResponseType(typeof(Subject), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<Subject>> CreateSubject(SubjectCreateDto subjectDto)
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


    [ProducesResponseType(typeof(Subject), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<Subject>> GetSubject(int id)
    {
        var subject = await _subjectService.ReadByIdAsync(id);

        if (subject == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok(subject);
    }

    [ProducesResponseType(typeof(Subject), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpPut]
    public async Task<ActionResult<Subject>> UpdateSubject(int subjectId, SubjectCreateDto updatedSubject)
    {
        var subject = await _subjectService.UpdateSubject(subjectId, _mapper.Map<Subject>(updatedSubject));

        if (subject == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok(subject);
    }

    [ProducesResponseType(typeof(Subject), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Subject>> DeleteSubject(int id)
    {
        var result = await _subjectService.DeleteSubject(id);

        if (!result)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return NoContent();
    }

}
