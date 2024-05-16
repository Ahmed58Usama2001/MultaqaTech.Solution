
namespace MultaqaTech.APIs.Controllers.EventEntitiesControllers
{
    public class EventSpeakersController(IEventService eventService, IEventSpeakerService eventSpeakerService , IMapper mapper ,
        IUnitOfWork unitOfWork) : BaseApiController
    {
        private readonly IEventService _eventService = eventService;
        private readonly IEventSpeakerService _eventSpeakerService = eventSpeakerService;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [ProducesResponseType(typeof(EventSpeakerToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<EventSpeaker>> CreateEventSpeaker(EventSpeakerCreateDto eventSpeakerDto)
        {
            if (eventSpeakerDto is null) return BadRequest(new ApiResponse(400));

            var existingEvent = await _eventService.ReadByIdAsync(eventSpeakerDto.EventId);
            if (existingEvent is null)
                return NotFound(new { Message = "Event wasn't Not Found", StatusCode = 404 });
            
            var mappedeventSpeaker = new EventSpeaker
            {
                
                Name= eventSpeakerDto.Name,
                JobTitle= eventSpeakerDto.JobTitle,
                SpeakerPictureUrl = DocumentSetting.UploadFile(eventSpeakerDto?.PictureUrl, "Events\\EventSpeakersImages"),
                EventId = eventSpeakerDto.EventId,
                Event = existingEvent,
               
            };

            var createdEventSpeaker = await _eventSpeakerService.CreateEventSpeakerAsync(mappedeventSpeaker);
            if (createdEventSpeaker is null) return BadRequest(new ApiResponse(400));

            return Ok(_mapper.Map<EventSpeaker, EventSpeakerToReturnDto>(createdEventSpeaker));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<EventSpeaker>>> GetAllEventSpeakers()
        {
            var eventSpeakers = await _eventSpeakerService.ReadAllEventSpeakersAsync();
            
            if (eventSpeakers == null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            return Ok(eventSpeakers);
        }


        [ProducesResponseType(typeof(EventSpeakerToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<EventSpeaker>> GetEventSpeaker(int id)
        {
            var eventSpeaker = await _eventSpeakerService.ReadByIdAsync(id);

            if (eventSpeaker == null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            return Ok(_mapper.Map<EventSpeakerToReturnDto>(eventSpeaker));
        }

        [ProducesResponseType(typeof(EventSpeakerToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpPut("{eventSpeakerId}")]
        public async Task<ActionResult<EventSpeaker>> UpdateEventSpeaker(int eventSpeakerId, EventSpeakerCreateDto updatedEventSpeakerDto)
        {
            var storedSpeaker = await _eventSpeakerService.ReadByIdAsync(eventSpeakerId);

            var updatedEvent = await _eventService.ReadByIdAsync(updatedEventSpeakerDto.EventId);
            if (updatedEvent is null)
                return NotFound(new { Message = "Event wasn't Not Found", StatusCode = 404 });

            if (storedSpeaker == null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            var newSpeaker = _mapper.Map<EventSpeakerCreateDto, EventSpeaker>(updatedEventSpeakerDto);
            newSpeaker.Id = storedSpeaker.Id;
            newSpeaker.Event = updatedEvent;
            newSpeaker.EventId = updatedEvent.Id;

            if (updatedEventSpeakerDto.PictureUrl is not null)
                newSpeaker.SpeakerPictureUrl = DocumentSetting.UploadFile(updatedEventSpeakerDto?.PictureUrl, "Events\\EventSpeakersImages");

            var eventSpeaker = await _eventSpeakerService.UpdateEventSpeaker(eventSpeakerId, newSpeaker);

            if (eventSpeaker == null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            return Ok(_mapper.Map<EventSpeakerToReturnDto>(eventSpeaker));
        }

        [ProducesResponseType(typeof(EventSpeakerToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<EventSpeaker>> DeleteEventSpeaker(int id)
        {

            var eventSpeaker = await _unitOfWork.Repository<EventSpeaker>().GetByIdAsync(id);
            if (eventSpeaker == null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });


            var result = await _eventSpeakerService.DeleteEventSpeaker(eventSpeaker);

            if (result)
            {
                if (!string.IsNullOrEmpty(eventSpeaker.SpeakerPictureUrl))
                    DocumentSetting.DeleteFile(eventSpeaker.SpeakerPictureUrl);

                return Ok(true);
            }

            return NoContent();
        }
    }
}
