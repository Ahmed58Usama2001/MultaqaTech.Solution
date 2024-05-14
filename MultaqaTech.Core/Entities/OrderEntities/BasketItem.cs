namespace MultaqaTech.Core.Entities.OrderEntities;

public class BasketItem : BaseEntityWithMediaUrl
{
    [JsonIgnore]
    public new int Id { get; set; }

    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public string CourseThumbnailUrl
    {
        get { return base.MediaUrl; }
        set { base.MediaUrl = value; }
    }

    public decimal Price { get; set; }
}