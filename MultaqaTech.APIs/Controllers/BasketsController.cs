namespace MultaqaTech.APIs.Controllers;

[Authorize]
public class BasketsController(IBasketRepository basketRepository, IMapper mapper) : BaseApiController
{
    private readonly IBasketRepository _basketRepository = basketRepository;
    private readonly IMapper _mapper = mapper;

    [ProducesResponseType(typeof(StudentBasket), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<StudentBasket>> UpdateStudentBasket(params int[] courseIds)
    {
        try
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email)) return BadRequest(new ApiResponse(401));

            StudentBasket? basket = await _basketRepository.UpdateBasket(email, courseIds);
            ManageBasketItemMediaUrl(basket);

            return basket is null ? BadRequest(new ApiResponse(400)) : Ok(basket);
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse(400, ex.Message));
        }
    }

    [ProducesResponseType(typeof(StudentBasket), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost("UpdateBasketWithBasketItem")]
    public async Task<ActionResult<StudentBasket>> UpdateStudentBasketWithBasketItem(int courseId)
    {
        try
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email)) return BadRequest(new ApiResponse(401));

            StudentBasket? basket = await _basketRepository.AddCourseToBasket(email, courseId);
            ManageBasketItemMediaUrl(basket);

            return basket == null ? BadRequest(new ApiResponse(400)) : Ok(basket);
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse(400, ex.Message));
        }
    }

    [ProducesResponseType(typeof(StudentBasket), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost("RemoveItemFromBasket")]
    public async Task<ActionResult<StudentBasket>> RemoveCourseFromBasket(int courseId)
    {
        string? email = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email)) return BadRequest(new ApiResponse(401));

        StudentBasket? basket = await _basketRepository.RemoveCourseFromBasket(email, courseId);
        ManageBasketItemMediaUrl(basket);

        return basket == null ? BadRequest(new ApiResponse(400)) : Ok(basket);
    }

    [ProducesResponseType(typeof(StudentBasket), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<StudentBasket>> GetStudentBasket()
    {
        string? email = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email)) return BadRequest(new ApiResponse(401));

        StudentBasket? basket = await _basketRepository.GetBasket(email);
        ManageBasketItemMediaUrl(basket);

        return basket is null ? new StudentBasket() : Ok(basket);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteStudentBasket()
    {
        string? email = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email))
            return BadRequest(new ApiResponse(401));

        bool isSuccess = await _basketRepository.DeleteBasket(email);

        return isSuccess ? NoContent() : NotFound(new ApiResponse(404));
    }

    private void ManageBasketItemMediaUrl(StudentBasket? basket)
    {
        if (basket is null || basket.BasketItems is null) return;

        for (int i = 0; i < basket.BasketItems.Count; i++)
        {
            basket.BasketItems[i] = _mapper.Map<BasketItem>(basket.BasketItems[i]);
        }
    }
}