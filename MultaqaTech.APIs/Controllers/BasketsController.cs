namespace MultaqaTech.APIs.Controllers;

public class BasketsController(IBasketRepository basketRepository) : BaseApiController
{
    private readonly IBasketRepository _basketRepository = basketRepository;

    [ProducesResponseType(typeof(StudentBasket),StatusCodes.Status200OK)]
    [HttpGet("{basketId}")]
    public async Task<ActionResult<StudentBasket>> GetStudentBasketById(string basketId)
    {
        StudentBasket? studentBasket = await _basketRepository.GetBasket(basketId);

        return studentBasket is null ? new StudentBasket(basketId) : Ok(studentBasket);
    }

    [ProducesResponseType(typeof(StudentBasket),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<ActionResult<StudentBasket>> UpdateStudentBasket(StudentBasket studentBasket)
    {
        StudentBasket? basket = await _basketRepository.UpdateBasket(studentBasket);
        return basket == null ? BadRequest(new ApiResponse(404)) : Ok(basket);
    }

    [HttpDelete("{basketId}")]
    public async Task<ActionResult> DeleteStudentBasket(string basketId)
    {
        bool isSuccess = await _basketRepository.DeleteBasket(basketId);

        return isSuccess ? NoContent() : NotFound(new ApiResponse(404));
    }
}