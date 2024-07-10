using MultaqaTech.Core.Specifications.OrderSpecs;

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

        var mappedOrder = _mapper.Map<Order>(orderDto);
        mappedOrder.UserEmail = email;
        Order? createdOrder = await _orderService.CreateOrderAsync(mappedOrder);

        return createdOrder is not null ? Ok(_mapper.Map<OrderToReturnDto>(createdOrder)) : BadRequest(new ApiResponse(400));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrders([FromQuery] OrderSpecificationsParams orderSpecificationsParams)
    {
        IEnumerable<Order>? orders = await _orderService.ReadOrdersAsync(orderSpecificationsParams);

        return Ok(_mapper.Map<IEnumerable<OrderToReturnDto>>(orders));
    }
}