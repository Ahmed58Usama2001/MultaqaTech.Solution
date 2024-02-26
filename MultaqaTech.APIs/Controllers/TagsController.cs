namespace MultaqaTech.APIs.Controllers;

public class TagsController(ITagService tagService, IMapper mapper) : BaseApiController
{
    private readonly ITagService _tagService = tagService;
    private readonly IMapper _mapper = mapper;


    [ProducesResponseType(typeof(Tag), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<Tag>> CreateTag(TagDto tagDto)
    {
        if (tagDto is null) return BadRequest(new ApiResponse(400));

        var createdTag = await _tagService.CreateTagAsync(_mapper.Map<Tag>(tagDto));

        if (createdTag is null) return BadRequest(new ApiResponse(400));

        return Ok(createdTag);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Tag>>> GetAllTags()
    {
        var tags = await _tagService.GetTagsAsync();
        return Ok(tags);
    }

    [ProducesResponseType(typeof(TagDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TagDto>> GetTag(int id)
    {
        var tag = await _tagService.GetTagAsync(id);

        if (tag == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok(_mapper.Map<Tag, TagDto>(tag));
    }

    [ProducesResponseType(typeof(Tag), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpPut]
    public async Task<ActionResult<Tag>> UpdateTag(int tagId, Tag updatedTag)
    {
        var tag = await _tagService.UpdateTag(tagId, updatedTag);

        if (tag == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok(_mapper.Map<Tag, TagDto>(tag));
    }

    [ProducesResponseType(typeof(Tag), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete]
    public async Task<ActionResult<Tag>> DeleteTag(int tagId)
    {
        var result = await _tagService.DeleteTag(tagId);

        if (!result )
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok();
    }

}