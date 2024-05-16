namespace MultaqaTech.APIs.Dtos.OrderDtos;

public class OrderDto
{
    public StudentBasket? Basket { get; set; }

    public string? Coupon { get; set; }

    public bool IsCouponApplied { get; set; }

    public PaymentType PaymentType { get; set; }
}