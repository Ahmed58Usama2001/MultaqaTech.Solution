
namespace MultaqaTech.Core.Entities.EventDomainEntities
{
    public class Event : BaseEntityWithMediaUrl
    {
        public string Title { get; set; } = string.Empty;
        public string AboutTheEvent { get; set; } = string.Empty;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public string EventPictureUrl
        {
            get { return base.MediaUrl; }
            set { base.MediaUrl = value; }
        }
        public int EventCategoryId { get; set; }
        public EventCategory Category { get; set; }
        public int EventCountryId { get; set; }
        public EventCountry Country { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string Price { get; set; }
        public List<EventComment>? Comments { get; set; } = new();
        public List<EventSpeaker>? Speakers { get; set; } = new();
    }
}
