namespace MultaqaTech.Core.Specifications.OrderSpecs;

public class OrderSpecifications : BaseSpecifications<Order>
{
    public OrderSpecifications(OrderSpecificationsParams orderParams) : base(e =>
           (string.IsNullOrEmpty(orderParams.UserEmail) || e.UserEmail == orderParams.UserEmail) &&
           (!orderParams.MinPrice.HasValue || e.PriceAfterDiscount > orderParams.MinPrice) &&
           (!orderParams.MaxPrice.HasValue || e.PriceAfterDiscount < orderParams.MaxPrice) &&
           (!orderParams.DateFrom.HasValue || e.CreationDate > orderParams.DateFrom) &&
           (!orderParams.DateTo.HasValue || e.CreationDate < orderParams.DateTo)
    )
    {
        ApplyPagination((orderParams.PageIndex - 1) * orderParams.PageSize, orderParams.PageSize);
    }
}