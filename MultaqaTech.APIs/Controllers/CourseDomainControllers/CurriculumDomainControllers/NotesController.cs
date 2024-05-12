using MultaqaTech.Core.Entities.BlogPostDomainEntities;
using MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

namespace MultaqaTech.APIs.Controllers.CurriculumItemDomainControllers.CurriculumDomainControllers;

[Authorize]
public class NotesController(
    IMapper mapper,
    UserManager<AppUser> userManager,
    INoteService noteService,
    ICurriculumItemService curriculumItemService,
    IUnitOfWork unitOfWork) : BaseApiController
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly INoteService _noteService = noteService;
    private readonly ICurriculumItemService _curriculumItemService = curriculumItemService;
    private readonly UserManager<AppUser> _userManager = userManager;

    [ProducesResponseType(typeof(NoteReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<NoteReturnDto>> CreateNoteAsync(NoteCreateDto noteDto)
    {
        if (noteDto is null) return BadRequest(new ApiResponse(400));

        CurriculumItem? existingLecture = await _curriculumItemService.ReadByIdAsync(noteDto.LectureId,CurriculumItemType.Lecture);
        if (existingLecture is null)
            return NotFound(new ApiResponse(404, "Lecture wasn't Not Found"));

        var mappedNote = _mapper.Map<NoteCreateDto, Note>(noteDto);
        mappedNote.Lecture = (Lecture)existingLecture;

        string? userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (userEmail is null) return NotFound(new ApiResponse(401));

        AppUser? storedUser = await _userManager.FindByEmailAsync(userEmail);
        if (storedUser is null) return NotFound(new ApiResponse(401));

        Student? student = await _unitOfWork.Repository<Student>().FindAsync(S => S.AppUserId == storedUser.Id);
        if (student is null)
            return NotFound(new ApiResponse(401));

        mappedNote.WriterStudentId = student.Id;

        var createdNote = await _noteService.CreateNoteAsync(mappedNote);

        if (createdNote is null) return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<Note, NoteReturnDto>(createdNote));
    }

    [ProducesResponseType(typeof(NoteReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    [Route("{LectureId}/notes")]
    public async Task<ActionResult<IReadOnlyList<NoteReturnDto>>> GetSignedInStudentNotesByLectureId(int LectureId)
    {
        string? userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (userEmail is null) return NotFound(new ApiResponse(401));

        AppUser? storedUser = await _userManager.FindByEmailAsync(userEmail);
        if (storedUser is null) return NotFound(new ApiResponse(401));

        Student? student = await _unitOfWork.Repository<Student>().FindAsync(S => S.AppUserId == storedUser.Id);
        if (student is null)
            return NotFound(new ApiResponse(401));

        NoteSpeceficationsParams speceficationsParams = new NoteSpeceficationsParams { lectureId = LectureId, writerStudentId=student.Id };
        if (speceficationsParams.lectureId <= 0)
            return BadRequest(new ApiResponse(404, "Enter a suitable Lecture ID: It must be greater than 0"));

        var notes = await _noteService.ReadNotesAsync(speceficationsParams);
        if (notes == null)
            return NotFound(new ApiResponse(404));

        var count = await _noteService.GetCountAsync(speceficationsParams);

        var data = _mapper.Map<IReadOnlyList<Note>, IReadOnlyList<NoteReturnDto>>(notes);

        return Ok(new Pagination<NoteReturnDto>(speceficationsParams.PageIndex, speceficationsParams.PageSize, count, data));
    }

    [ProducesResponseType(typeof(NoteReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult<NoteReturnDto>> UpdateNote(int id, NoteUpdateDto updatedNoteDto)
    {
        Note? storedNote = await _noteService.ReadByIdAsync(id);

        if (storedNote == null)
            return NotFound(new ApiResponse(404));

        if (!await CheckIfRequestFromCreatorUser(storedNote.WriterStudentId))
            return BadRequest(new ApiResponse(401));

        CurriculumItem? existingLecture = await _curriculumItemService.ReadByIdAsync(storedNote.LectureId, CurriculumItemType.Lecture);
        if (existingLecture is null)
            return NotFound(new ApiResponse(404, "Lecture wasn't Not Found"));

        Note newNote = _mapper.Map<NoteUpdateDto, Note>(updatedNoteDto);
        newNote.Id = storedNote.Id;
        newNote.Lecture = (Lecture)existingLecture;
        newNote.LectureId = existingLecture.Id;

        storedNote = await _noteService.UpdateNote(storedNote, newNote);

        if (storedNote == null)
            return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<NoteReturnDto>(storedNote));
    }

    [ProducesResponseType(typeof(NoteReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        Note? storedNote = await _noteService.ReadByIdAsync(id);


        if (!await CheckIfRequestFromCreatorUser(storedNote.WriterStudentId))
            return BadRequest(new ApiResponse(401));
        
        var result = await _noteService.DeleteNote(id);

        if (result)
            return Ok(true);

        return BadRequest(new ApiResponse(400));
    }

    private async Task<bool> CheckIfRequestFromCreatorUser(int noteWriterId)
    {
        string? userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (userEmail is null) return false;

        AppUser? storedUser = await _userManager.FindByEmailAsync(userEmail);
        if (storedUser is null) return false;

        Student? student = await _unitOfWork.Repository<Student>().FindAsync(S => S.AppUserId == storedUser.Id);
        if (student is null)
            return false;

        if (student.Id != noteWriterId)
            return false;

        return true;
    }

}
