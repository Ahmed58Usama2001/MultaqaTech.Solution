namespace MultaqaTech.Core.Entities.OrderEntities;

public class Order : BaseEntity
{
    public StudentBasket? Basket { get; set; }

    public string UserEmail { get; set; } = string.Empty;
    public OrderStatus Status { get; set; }
    public string? Coupon { get; set; }

    public bool IsCouponApplied { get; set; }

    public PaymentType PaymentType { get; set; }

    public DateTime CreationDate { get; set; }

    public decimal TotalPriceBeforeDiscount { get; set; }
    public decimal PriceAfterDiscount { get; set; }
}