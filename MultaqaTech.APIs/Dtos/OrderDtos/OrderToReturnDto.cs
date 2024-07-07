namespace MultaqaTech.APIs.Dtos.OrderDtos;

public class OrderToReturnDto
{
    public StudentBasket? Basket { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime CreationDate { get; set; }

    public decimal TotalPriceBeforeDiscount { get; set; }
    public decimal PriceAfterDiscount { get; set; }
}