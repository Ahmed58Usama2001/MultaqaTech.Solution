namespace MultaqaTech.Core.Entities;

public abstract class BaseEntityWithPictureUrl : BaseEntity 
{
    public virtual string PictureUrl { get; set; }
}