namespace MultaqaTech.Core.Entities;

public abstract class BaseEntityWithMediaUrl : BaseEntity 
{
    public virtual string MediaUrl { get; set; }
}