namespace MultaqaTech.APIs.Controllers.ZoomMeetingEntitiesControllers;


[Authorize]
public class ZoomMeetingController(IZoomMeetingService zoomMeetingService, IMapper mapper, UserManager<AppUser> userManager, IZoomMeetingCategoryService zoomMeetingCategoryService
    , IUnitOfWork unitOfWork, IConfiguration configuration) : BaseApiController
{
    private readonly IZoomMeetingService _zoomMeetingService = zoomMeetingService;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IZoomMeetingCategoryService _zoomMeetingCategoryService = zoomMeetingCategoryService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient httpClient = new HttpClient();


    [ProducesResponseType(typeof(ZoomMeetingToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<ZoomMeetingToReturnDto>> CreateZoomMeetingAsync(ZoomMeetingCreateDto zoomMeetingDto)
    {
        if (zoomMeetingDto is null) return BadRequest(new ApiResponse(400));

        var authorEmail = User.FindFirstValue(ClaimTypes.Email);
        if (authorEmail is null) return BadRequest(new ApiResponse(404));

        var user = await _userManager.FindByEmailAsync(authorEmail);
        if (user is null) return BadRequest(new ApiResponse(404));

        var existingCategory = await _zoomMeetingCategoryService.ReadByIdAsync(zoomMeetingDto.CategoryId);
        if (existingCategory is null)
            return NotFound(new { Message = "Category wasn't Not Found", StatusCode = 404 });


        var accessToken = await GenerateToken();
        var requestBody = new
        {
            topic = zoomMeetingDto.Title,
            type = 2, // 2 for scheduled meetings
            start_time = zoomMeetingDto.StartDate.ToString("yyyy-MM-dd'T'HH:mm:ss"),
            duration = zoomMeetingDto.Duration,
        };

        var requestContent = new StringContent(JsonConvert.SerializeObject(requestBody),
                                              Encoding.UTF8,
                                              "application/json");


        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        Meeting meetingData;
        try
        {
            string url = "https://api.zoom.us/v2/users/me/meetings";
            var response = await httpClient.PostAsync(url, requestContent);
            if (response.IsSuccessStatusCode)
            {
                meetingData = (JsonConvert.DeserializeObject<Meeting>(await response?.Content?.ReadAsStringAsync()));

            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return BadRequest(errorMessage);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);

        }
        var mappedzoomMeeting = new ZoomMeeting
        {
            Title = zoomMeetingDto.Title,
            AuthorName = user.UserName,
            Content = zoomMeetingDto.Content,
            ZoomPictureUrl = zoomMeetingDto.PictureUrl,
            ZoomMeetingCategoryId = zoomMeetingDto.CategoryId,
            Category = existingCategory,
            Duration = zoomMeetingDto.Duration,
            StartDate = zoomMeetingDto.StartDate,
            TimeZone = zoomMeetingDto.TimeZone,
            MeetingUrl = meetingData?.join_url ?? string.Empty,
            MeetingId = meetingData?.id ?? string.Empty,

        };

        var createdZoomMeeting = await zoomMeetingService.CreateZoomMeetingAsync(mappedzoomMeeting);
        if (createdZoomMeeting is null) return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<ZoomMeeting, ZoomMeetingToReturnDto>(createdZoomMeeting));
    }

    [ProducesResponseType(typeof(ZoomMeetingToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ZoomMeetingToReturnDto>>> GetZoomMeetings([FromQuery] ZoomMeetingSpeceficationsParams speceficationsParams)
    {
        var zoomMeetings = await _zoomMeetingService.ReadZoomMeetingAsync(speceficationsParams);

        if (zoomMeetings == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        var count = await _zoomMeetingService.GetCountAsync(speceficationsParams);

        var data = _mapper.Map<IReadOnlyList<ZoomMeeting>, IReadOnlyList<ZoomMeetingToReturnDto>>(zoomMeetings);

        return Ok(new Pagination<ZoomMeetingToReturnDto>(speceficationsParams.PageIndex, speceficationsParams.PageSize, count, data));
    }

    [ProducesResponseType(typeof(ZoomMeetingToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ZoomMeetingToReturnDto>> GetZoomMeeting(int id)
    {
        var zoomMeeting = await _zoomMeetingService.ReadByIdAsync(id);

        if (zoomMeeting == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok(_mapper.Map<ZoomMeetingToReturnDto>(zoomMeeting));
    }

    [ProducesResponseType(typeof(ZoomMeetingToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpPut("{ZoomMeetingId}")]
    public async Task<ActionResult<ZoomMeetingToReturnDto>> UpdateZoomMeeting(int zoomMeetingId, ZoomMeetingCreateDto updatedZoomMeetingDto)
    {
        var updatedMeeting = await _zoomMeetingService.ReadByIdAsync(zoomMeetingId);

        updatedMeeting.Title = updatedZoomMeetingDto.Title;
        updatedMeeting.Content = updatedZoomMeetingDto.Content;
        updatedMeeting.ZoomPictureUrl = updatedZoomMeetingDto.PictureUrl;
        updatedMeeting.TimeZone = updatedZoomMeetingDto.TimeZone;

        updatedMeeting.ZoomMeetingCategoryId = updatedZoomMeetingDto.CategoryId;
        var existingCategory = await _zoomMeetingCategoryService.ReadByIdAsync(updatedZoomMeetingDto.CategoryId);
        if (existingCategory is null)
            return NotFound(new { Message = "Category wasn't Not Found", StatusCode = 404 });
        updatedMeeting.Category = existingCategory;


        var zoomMeeting = await _zoomMeetingService.UpdateZoomMeeting(zoomMeetingId, updatedMeeting);

        if (zoomMeeting == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok(_mapper.Map<ZoomMeetingToReturnDto>(zoomMeeting));
    }

    [ProducesResponseType(typeof(ZoomMeetingToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ZoomMeetingToReturnDto>> DeleteZoomMeeting(int id)
    {
        var zoomMeeting = await _unitOfWork.Repository<ZoomMeeting>().GetByIdAsync(id);
        if (zoomMeeting == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        var authorEmail = User.FindFirstValue(ClaimTypes.Email);
        if (authorEmail is null) return BadRequest(new ApiResponse(404));

        var user = await _userManager.FindByEmailAsync(authorEmail);
        if (user is null || user.UserName != zoomMeeting.AuthorName)
            return BadRequest(new ApiResponse(401));

        var result = await _zoomMeetingService.DeleteZoomMeeting(zoomMeeting);

        if (!result)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return NoContent();
    }

    private async Task<string> GenerateToken()
    {
        httpClient.BaseAddress = new Uri("https://zoom.us/oauth/token");
        var request = new HttpRequestMessage(HttpMethod.Post, string.Empty);

        string clientId = _configuration["Zoom:clientId"];
        string clientSecret = _configuration["Zoom:clientSecret"];
        string accountId = _configuration["Zoom:accountId"];

        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}")));

        request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
    {
        { "grant_type", "account_credentials" },
        { "account_id", accountId }
    });

        request.Headers.Host = "zoom.us";

        var response = await httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<ZoomAccessToken>(content);
            return token.access_token;
        }
        else
        {

            Console.WriteLine($"Failed to retrieve access token: {response.ReasonPhrase}");
            return null;
        }
    }


    public class ZoomAccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }

    public class Meeting
    {
        public string id { get; set; }
        public string join_url { get; set; }
    }

}