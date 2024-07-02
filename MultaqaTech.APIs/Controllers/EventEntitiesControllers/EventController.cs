namespace MultaqaTech.APIs.Controllers.EventEntitiesControllers
{
    [Authorize]
    public class EventController(IEventService eventService , IMapper mapper , UserManager<AppUser> userManager , IEventCategoryService eventCategoryService ,
     IEventCountryService eventCountryService  , IEventSpeakerService eventSpeakerService , IUnitOfWork unitOfWork  ) : BaseApiController
    {
        private readonly IEventService _eventService = eventService;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IEventCategoryService _eventCategoryService = eventCategoryService;
        private readonly IEventCountryService _eventCountryService = eventCountryService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [ProducesResponseType(typeof(EventToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]

        public async Task<ActionResult<EventToReturnDto>> CreateEventAsync(EventCreateDto eventDto)
        {
            if (eventDto is null) return BadRequest(new ApiResponse(400));

            var authorEmail = User.FindFirstValue(ClaimTypes.Email);
            if (authorEmail is null) return BadRequest(new ApiResponse(404));

            var user = await _userManager.FindByEmailAsync(authorEmail);
            if (user is null) return BadRequest(new ApiResponse(404));

            var existingCategory = await _eventCategoryService.ReadByIdAsync(eventDto.CategoryId);
            if (existingCategory is null)
                return NotFound(new { Message = "Category wasn't Not Found", StatusCode = 404 });

            var existingCountry = await _eventCountryService.ReadByIdAsync(eventDto.CountryId);
            if (existingCountry is null)
                return NotFound(new { Message = "Country wasn't Not Found", StatusCode = 404 });

            var mappedEvent = new Event
            {
                Title = eventDto.Title,
                AboutTheEvent = eventDto.AboutTheEvent,
                EventBy= user.UserName,
                PhoneNumber = eventDto.PhoneNumber,
                Price = eventDto.Price,
                Address = eventDto.Address,
                Website = eventDto.Website,
                EventPictureUrl = DocumentSetting.UploadFile(eventDto?.PictureUrl, "Events\\EventsImages"),
                DateFrom= eventDto.DateFrom,
                DateTo= eventDto.DateTo,
                TimeFrom= eventDto.TimeFrom,
                TimeTo= eventDto.TimeTo,
                EventCategoryId = eventDto.CategoryId,
                Category=existingCategory,
                EventCountryId = eventDto.CountryId,
                Country =existingCountry,

            };

            var createdEvent = await eventService.CreateEventAsync(mappedEvent);

            if (createdEvent is null) return BadRequest(new ApiResponse(400));

            return Ok(_mapper.Map<Event, EventToReturnDto>(createdEvent));
        }

        [ProducesResponseType(typeof(EventToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<Pagination<EventToReturnDto>>> GetEvents([FromQuery] EventSpeceficationsParams speceficationsParams)
        {
            var events = await _eventService.ReadAllEventsAsync(speceficationsParams);

            if (events == null)
                return NotFound(new ApiResponse(404));

            var count = await _eventService.GetCountAsync(speceficationsParams);

            var data = _mapper.Map<IReadOnlyList<Event>, IReadOnlyList<EventToReturnDto>>(events);

            return Ok(new Pagination<EventToReturnDto>(speceficationsParams.PageIndex, speceficationsParams.PageSize, count, data));
        }

        [ProducesResponseType(typeof(EventToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<EventToReturnDto>> GetEvent(int id)
        {
            var @event = await _eventService.ReadByIdAsync(id);

            if (@event == null)
                return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<EventToReturnDto>(@event));
        }

        [ProducesResponseType(typeof(EventToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpPut("{eventId}")]
        public async Task<ActionResult<EventToReturnDto>> UpdateEvent(int eventId, EventCreateDto updatedEventDto)
        {
            var storedEvent = await _eventService.ReadByIdAsync(eventId);

            var updatedCategory = await _eventCategoryService.ReadByIdAsync(updatedEventDto.CategoryId);
            if (updatedCategory is null)
                return NotFound(new { Message = "Category wasn't Not Found", StatusCode = 404 });

            var updatedCountry = await _eventCountryService.ReadByIdAsync(updatedEventDto.CountryId);
            if (updatedCountry is null)
                return NotFound(new { Message = "Country wasn't Not Found", StatusCode = 404 });

            if (storedEvent == null)
                return NotFound(new ApiResponse(404));

            var authorEmail = User.FindFirstValue(ClaimTypes.Email);
            if (authorEmail is null) return BadRequest(new ApiResponse(404));

            var user = await _userManager.FindByEmailAsync(authorEmail);
            if (user is null || user.UserName != storedEvent?.EventBy)
                return BadRequest(new ApiResponse(401));

            if (!string.IsNullOrEmpty(storedEvent?.EventPictureUrl))
                DocumentSetting.DeleteFile(storedEvent.EventPictureUrl);


            var newEvent = _mapper.Map<EventCreateDto, Event>(updatedEventDto);
            newEvent.Id = storedEvent.Id;
            newEvent.Category = updatedCategory;
            newEvent.EventCategoryId = updatedCategory.Id;
            newEvent.Country = updatedCountry;
            newEvent.EventCountryId = updatedCategory.Id;


            if (updatedEventDto.PictureUrl is not null)
                newEvent.EventPictureUrl = DocumentSetting.UploadFile(updatedEventDto?.PictureUrl, "Events\\EventsImages");

            storedEvent = await _eventService.UpdateEvent(storedEvent, newEvent);

            if (storedEvent == null)
                return BadRequest(new ApiResponse(400));

            return Ok(_mapper.Map<EventToReturnDto>(storedEvent));
        }


        [ProducesResponseType(typeof(EventToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var @event = await _unitOfWork.Repository<Event>().GetByIdAsync(id);
            if (@event == null)
                return NotFound(new ApiResponse(404));

            var authorEmail = User.FindFirstValue(ClaimTypes.Email);
            if (authorEmail is null) return BadRequest(new ApiResponse(404));

            var user = await _userManager.FindByEmailAsync(authorEmail);
            if (user is null || user.UserName != @event.EventBy)
                return BadRequest(new ApiResponse(401));


            var result = await _eventService.DeleteEvent(@event);

            if (result)
            {
                if (!string.IsNullOrEmpty(@event.EventPictureUrl))
                    DocumentSetting.DeleteFile(@event.EventPictureUrl);

                return Ok(true);
            }

            return BadRequest(new ApiResponse(400));
        }


    }
}
