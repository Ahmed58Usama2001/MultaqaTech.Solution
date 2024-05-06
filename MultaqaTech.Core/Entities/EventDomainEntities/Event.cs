
namespace MultaqaTech.Core.Entities.EventDomainEntities
{
    public class Event : BaseEntityWithMediaUrl
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Country {  get; set; }
        public DateTime StartDate { get; set; }
        public string EventPictureUrl
        {
            get { return base.MediaUrl; }
            set { base.MediaUrl = value; }
        }
        public int EventCategoryId { get; set; }
        public EventCategory Category { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public decimal Price { get; set; }
    }
}
