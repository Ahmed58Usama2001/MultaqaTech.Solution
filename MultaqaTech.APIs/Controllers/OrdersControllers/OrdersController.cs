namespace MultaqaTech.APIs.Controllers;

[Authorize]
public class OrdersController(IOrderService orderService, IMapper mapper) : BaseApiController
{
    private readonly IMapper _mapper = mapper;
    private readonly IOrderService _orderService = orderService;

    [HttpPost]
    public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
    {
        string? email = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email))
            return BadRequest(new ApiResponse(401));

        Order? createdOrder = await _orderService.CreateOrderAsync(_mapper.Map<Order>(orderDto));

        return createdOrder is null ? Ok(_mapper.Map<OrderToReturnDto>(createdOrder)) : BadRequest(new ApiResponse(400));
    }
}