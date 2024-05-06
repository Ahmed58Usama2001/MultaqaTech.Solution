namespace MultaqaTech.Core.Entities.BasketEntities;

public class StudentBasket(string id)
{
    public string? Id { get; set; } = id;
    public List<BasketItem>? BasketItems { get; set; }
}