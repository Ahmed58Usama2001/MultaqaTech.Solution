
namespace MultaqaTech.Core.Entities.EventDomainEntities
{
    public class EventCategory : BaseEntity
    {
        public string Name { get; set; }

        [JsonIgnore]
        public List<Event> Events { get; set;} = new();
    }
}
