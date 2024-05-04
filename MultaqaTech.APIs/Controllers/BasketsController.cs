namespace MultaqaTech.APIs.Controllers;

public class BasketsController(IBasketRepository basketRepository) : BaseApiController
{
    private readonly IBasketRepository _basketRepository = basketRepository;

    [HttpGet("{basketId}")]
    public async Task<ActionResult<StudentBasket>> GetCustomerBasketById(string basketId)
    {
        StudentBasket? studentBasket = await _basketRepository.GetBasket(basketId);
        if (studentBasket is not null)
            return Ok(studentBasket);
        else
            return new StudentBasket(basketId);
    }

    [HttpPost]
    public async Task<ActionResult<StudentBasket>> UpdateCustomerBasket(StudentBasket studentBasket)
    {
        StudentBasket? basket = await _basketRepository.UpdateBasket(studentBasket);
        return basket == null ? BadRequest(new ApiResponse(400)) : Ok(basket);
    }

    [HttpDelete("{basketId}")]
    public async Task<ActionResult> DeleteCustomerBasket(string basketId)
    {
        bool isSuccess = await _basketRepository.DeleteBasket(basketId);

        return isSuccess ? NoContent() : NotFound(new ApiResponse(404));
    }
}