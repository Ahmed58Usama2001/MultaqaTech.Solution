namespace MultaqaTech.Core.Entities.BasketEntities;

public class BasketItem : BaseEntityWithMediaUrl
{
    public string CourseId { get; set; } = string.Empty;
    public string Course { get; set; } = string.Empty;

    public decimal Price { get; set; }
}