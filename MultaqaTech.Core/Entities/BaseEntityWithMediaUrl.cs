namespace MultaqaTech.Core.Entities;

public abstract class BaseEntityWithMediaUrl : BaseEntity
{
    [JsonIgnore]
    public virtual string MediaUrl { get; set; } = string.Empty;
}