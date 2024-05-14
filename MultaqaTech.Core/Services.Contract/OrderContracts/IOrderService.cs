namespace MultaqaTech.Core.Services.Contract.OrderContracts;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Order order);
}