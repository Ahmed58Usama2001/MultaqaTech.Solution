namespace MultaqaTech.APIs.Controllers;

[Authorize]
public class BasketsController(IBasketRepository basketRepository) : BaseApiController
{
    private readonly IBasketRepository _basketRepository = basketRepository;

    [ProducesResponseType(typeof(StudentBasket), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<StudentBasket>> UpdateStudentBasket(StudentBasket studentBasket)
    {
        string? email = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email))
            return BadRequest(new ApiResponse(401));

        StudentBasket? basket = await _basketRepository.UpdateBasket(studentBasket, email);
        return basket == null ? BadRequest(new ApiResponse(400)) : Ok(basket);
    }

    [ProducesResponseType(typeof(StudentBasket), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost("UpdateBasketWithBasketItem")]
    public async Task<ActionResult<StudentBasket>> UpdateStudentBasketWithBasketItem(BasketItem basketItem)
    {
        string? email = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email))
            return BadRequest(new ApiResponse(401));

        StudentBasket? basket = await _basketRepository.AddCourseToBasket(email, basketItem);
        return basket == null ? BadRequest(new ApiResponse(400)) : Ok(basket);
    }

    [ProducesResponseType(typeof(StudentBasket), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost("RemoveItemFromBasket")]
    public async Task<ActionResult<StudentBasket>> RemoveCourseFromBasket(BasketItem basketItem)
    {
        string? email = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email))
            return BadRequest(new ApiResponse(401));

        StudentBasket? basket = await _basketRepository.RemoveCourseFromBasket(email, basketItem);
        return basket == null ? BadRequest(new ApiResponse(400)) : Ok(basket);
    }


    [ProducesResponseType(typeof(StudentBasket), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<StudentBasket>> GetStudentBasket()
    {
        string? email = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email))
            return BadRequest(new ApiResponse(401));

        StudentBasket? studentBasket = await _basketRepository.GetBasket(email);

        return studentBasket is null ? new StudentBasket() : Ok(studentBasket);
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
}