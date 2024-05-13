namespace MultaqaTech.Core.Services.Contract.OrderContracts;

public interface IOrderService
{
    Task<bool> CreateOrderAsync(Order order);
}