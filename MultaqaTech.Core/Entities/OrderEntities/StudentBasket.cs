namespace MultaqaTech.Core.Entities.OrderEntities;

public class StudentBasket()
{
    public int StudentId { get; set; } 
    public List<BasketItem>? BasketItems { get; set; }
}