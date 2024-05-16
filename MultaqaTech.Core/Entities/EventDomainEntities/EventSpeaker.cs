
namespace MultaqaTech.Core.Entities.EventDomainEntities
{
    public class EventSpeaker : BaseEntityWithMediaUrl
    {
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string SpeakerPictureUrl
        {
            get { return base.MediaUrl; }
            set { base.MediaUrl = value; }
        }

        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
