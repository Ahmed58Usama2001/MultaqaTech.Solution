

namespace MultaqaTech.Core.Entities.EventDomainEntities
{
    public class EventCountry : BaseEntity
    {
        public string Name { get; set; }

        [JsonIgnore]
        public List<Event> Events { get; set; } = new();
    }
}
