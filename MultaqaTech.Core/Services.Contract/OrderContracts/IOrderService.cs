using MultaqaTech.Core.Specifications.OrderSpecs;

namespace MultaqaTech.Core.Services.Contract.OrderContracts;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Order order);
    Task<IEnumerable<Order>> ReadOrdersAsync(OrderSpecificationsParams orderSpecificationsParams);
}