
using MultaqaTech.Core.Specifications.EventEntitiesSpecs;

namespace MultaqaTech.Core.Services.Contract.EventContracts
{
    public interface IEventService
    {
        Task<Event?> CreateEventAsync(Event @event);

        Task<IReadOnlyList<Event>> ReadAllEventsAsync(EventSpeceficationsParams speceficationsParams);

        Task<Event?> ReadByIdAsync(int eventId);

        Task<Event?> UpdateEvent(Event storedEvent, Event newEvent);

        Task<bool> DeleteEvent(Event @event);

        Task<int> GetCountAsync(EventSpeceficationsParams speceficationsParams);
    }
}
