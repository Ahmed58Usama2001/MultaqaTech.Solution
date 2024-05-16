
namespace MultaqaTech.APIs.Controllers.EventEntitiesControllers
{
    [Authorize]
    public class EventCommentsController(IEventService eventService, IMapper mapper, UserManager<AppUser> userManager, IEventCommentService eventCommentService
    , IUnitOfWork unitOfWork) : BaseApiController
    {
        private readonly IEventService _eventService = eventService;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IEventCommentService _eventCommentService = eventCommentService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [ProducesResponseType(typeof(EventCommentToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<EventCommentToReturnDto>> CreateEventAsync(EventCommentCreateDto eventCommentDto)
        {
            if (eventCommentDto is null) return BadRequest(new ApiResponse(400));

            var authorEmail = User.FindFirstValue(ClaimTypes.Email);
            if (authorEmail is null) return BadRequest(new ApiResponse(404));

            var user = await _userManager.FindByEmailAsync(authorEmail);
            if (user is null) return BadRequest(new ApiResponse(404));

            var existingEvent = await _eventService.ReadByIdAsync(eventCommentDto.EventId);
            if (existingEvent is null)
                return NotFound(new { Message = "Event wasn't Not Found", StatusCode = 404 });

            var mappedeventComment = new EventComment
            {
                AuthorName = user.UserName,
                CommentContent = eventCommentDto.CommentContent,
                EventId = eventCommentDto.EventId,
                Event = existingEvent,
                DatePosted = DateTime.Now,
            };

            var createdEventComment = await eventCommentService.CreateEventCommentAsync(mappedeventComment);

            if (createdEventComment is null) return BadRequest(new ApiResponse(400));

            return Ok(_mapper.Map<EventComment, EventCommentToReturnDto>(createdEventComment));
        }


        [ProducesResponseType(typeof(EventCommentToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<Pagination<EventCommentToReturnDto>>> GetBlogPostComments([FromQuery] EventCommentSpeceficationsParams speceficationsParams)
        {
            var eventComments = await _eventCommentService.ReadAllEventCommentsAsync(speceficationsParams);
            if (eventComments == null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            var count = await _eventCommentService.GetCountAsync(speceficationsParams);

            var data = _mapper.Map<IReadOnlyList<EventComment>, IReadOnlyList<EventCommentToReturnDto>>(eventComments);

            return Ok(new Pagination<EventCommentToReturnDto>(speceficationsParams.PageIndex, speceficationsParams.PageSize, count, data));
        }

        [ProducesResponseType(typeof(EventCommentToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<EventCommentToReturnDto>> GetEventComment(int id)
        {
            var eventComment = await _eventCommentService.ReadByIdAsync(id);

            if (eventComment == null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            return Ok(_mapper.Map<EventCommentToReturnDto>(eventComment));
        }

        [ProducesResponseType(typeof(EventCommentToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpPut("{eventCommentId}")]
        public async Task<ActionResult<EventCommentToReturnDto>> UpdateEventComment(int eventCommentId, EventCommentCreateDto updatedEventCommentDto)
        {
            var updatedComment = await _eventCommentService.ReadByIdAsync(eventCommentId);

            if (updatedComment == null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            updatedComment.CommentContent = updatedEventCommentDto.CommentContent;

            var eventComment = await _eventCommentService.UpdateEventComment(eventCommentId, updatedComment);

            if (eventComment == null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            return Ok(_mapper.Map<EventCommentToReturnDto>(eventComment));
        }


        [ProducesResponseType(typeof(EventCommentToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<EventCommentToReturnDto>> DeleteEventComment(int id)
        {
            var eventComment = await _unitOfWork.Repository<EventComment>().GetByIdAsync(id);
            if (eventComment == null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });


            var authorEmail = User.FindFirstValue(ClaimTypes.Email);
            if (authorEmail is null) return BadRequest(new ApiResponse(404));

            var user = await _userManager.FindByEmailAsync(authorEmail);
            if (user is null || user.UserName != eventComment.AuthorName)
                return BadRequest(new ApiResponse(401));

            var result = await _eventCommentService.DeleteEventComment(eventComment);

            if (!result)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            return NoContent();
        }
    }
}
