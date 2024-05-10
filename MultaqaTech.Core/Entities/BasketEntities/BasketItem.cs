namespace MultaqaTech.Core.Entities.BasketEntities;

public class BasketItem : BaseEntityWithMediaUrl
{
    [JsonIgnore]
    public new int Id { get; set; }

    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;

    public decimal Price { get; set; }
}