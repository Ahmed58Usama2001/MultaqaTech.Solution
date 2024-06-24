
namespace MultaqaTech.APIs.Controllers.EventEntitiesControllers
{
    [Authorize]
    public class EventCountriesController(IEventCountryService eventCountryService , IMapper mapper) : BaseApiController
    {
        private readonly IEventCountryService _eventCountryService = eventCountryService;
        private readonly IMapper _mapper = mapper;

        [ProducesResponseType(typeof(EventCountry), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<EventCountry>> CreateEventCountry(EventCountryCreateDto eventCountryDto)
        {
            if (eventCountryDto is null) return BadRequest(new ApiResponse(400));
            var createdCountry = await _eventCountryService.CreateEventCountryAsync(_mapper.Map<EventCountry>(eventCountryDto));

            if (createdCountry is null) return BadRequest(new ApiResponse(400));

            return Ok(createdCountry);
        }

        [ProducesResponseType(typeof(EventCountry), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<EventCountry>>> GetAllEventCountries()
        {
            var countries = await _eventCountryService.ReadAllAsync();

            if (countries == null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            return Ok(countries);
        }
        [ProducesResponseType(typeof(EventCountry), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<EventCountry>> GetEventCountry(int id)
        {
            var country = await _eventCountryService.ReadByIdAsync(id);

            if (country == null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            return Ok(country);
        }

        [ProducesResponseType(typeof(EventCountry), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpPut("{countryId}")]

        public async Task<ActionResult<EventCountry>> UpdateEventCountry(int countryId, EventCountryCreateDto updatedCountry)
        {
            var country = await _eventCountryService.UpdateEventCountry(countryId, _mapper.Map<EventCountry>(updatedCountry));

            if (country == null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            return Ok(country);
        }

        [ProducesResponseType(typeof(EventCountry), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<EventCountry>> DeleteEventCountry(int id)
        {
            var result = await _eventCountryService.DeleteEventCountry(id);

            if (!result)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            return NoContent();
        }
    }   
}
