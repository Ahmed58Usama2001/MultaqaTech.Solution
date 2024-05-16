namespace MultaqaTech.APIs.Dtos.OrderDtos;

public class OrderToReturnDto
{
    public OrderStatus Status { get; set; }

    public PaymentType PaymentType { get; set; }

    public DateTime CreationDate { get; set; }

    public decimal TotalPriceBeforeDiscount { get; set; }
    public decimal PriceAfterDiscount { get; set; }
}