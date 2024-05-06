
namespace MultaqaTech.APIs.Controllers.EventEntitiesControllers
{
    [Authorize]
    public class EventCategoriesController(IEventCategoryService eventCategoryService , IMapper mapper) : BaseApiController
    {
        private readonly IEventCategoryService _eventCategoryService = eventCategoryService;
        private readonly IMapper _mapper = mapper;

        [ProducesResponseType(typeof(EventCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<EventCategory>> CreateEventCategory(EventCategoryCreateDto eventCategoryDto)
        {
            if (eventCategoryDto is null) return BadRequest(new ApiResponse(400));
            var createdCategory = await _eventCategoryService.CreateEventCategoryAsync(_mapper.Map<EventCategory>(eventCategoryDto));

            if (createdCategory is null) return BadRequest(new ApiResponse(400));

            return Ok(createdCategory);
        }

        [ProducesResponseType(typeof(EventCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<EventCategory>>> GetAllEventCategories()
        {
            var categories = await _eventCategoryService.ReadAllAsync();

            if (categories == null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            return Ok(categories);
        }
        [ProducesResponseType(typeof(EventCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<EventCategory>> GetEventCategory(int id)
        {
            var category = await _eventCategoryService.ReadByIdAsync(id);

            if (category == null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            return Ok(category);
        }
        [ProducesResponseType(typeof(EventCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpPut("{categoryId}")]

        public async Task<ActionResult<EventCategory>> UpdateEventCategory(int categoryId, EventCategoryCreateDto updatedCategory)
        {
            var category = await _eventCategoryService.UpdateEventCategory(categoryId, _mapper.Map<EventCategory>(updatedCategory));

            if (category == null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            return Ok(category);
        }

        [ProducesResponseType(typeof(EventCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<EventCategory>> DeleteEventCategory(int id)
        {
            var result = await _eventCategoryService.DeleteEventCategory(id);

            if (!result)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            return NoContent();
        }
    }
}
