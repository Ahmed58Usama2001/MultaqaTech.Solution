
using MultaqaTech.Core.Specifications.EventEntitiesSpecs;

namespace MultaqaTech.Service.EventEntitesServices
{
    public class EventService(IUnitOfWork unitOfWork) : IEventService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Event?> CreateEventAsync(Event @event)
        {
            try
            {
                await _unitOfWork.Repository<Event>().AddAsync(@event);
                var result = await _unitOfWork.CompleteAsync();
                if (result <= 0) return null;

                return @event;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }

        public async Task<bool> DeleteEvent(Event @event)
        {
            try
            {
                _unitOfWork.Repository<Event>().Delete(@event);

                var result = await _unitOfWork.CompleteAsync();

                if (result <= 0)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return false;
            }
        }

        public async Task<int> GetCountAsync(EventSpeceficationsParams speceficationsParams)
        {
            var countSpec = new EventWithFilterationForCountSpecifications(speceficationsParams);

            var count = await _unitOfWork.Repository<Event>().GetCountAsync(countSpec);

            return count;
        }

       public async Task<IReadOnlyList<Event>> ReadAllEventsAsync(EventSpeceficationsParams speceficationsParams)
        {
            var spec = new EventWithIncludesSpecifications(speceficationsParams);

            var events = await _unitOfWork.Repository<Event>().GetAllWithSpecAsync(spec);

            return events;
        }

       public async Task<Event?> ReadByIdAsync(int eventId)
        {
            var spec = new EventWithIncludesSpecifications(eventId);

            var @event = await _unitOfWork.Repository<Event>().GetByIdWithSpecAsync(spec);

            return @event;
        }
     
       public async Task<Event?> UpdateEvent(Event storedEvent, Event newEvent)
        {
            if (storedEvent == null || newEvent == null)
                return null;
            storedEvent.Title = newEvent.Title;
            storedEvent.Content = newEvent.Content;
            storedEvent.Price = newEvent.Price;
            storedEvent.StartDate = newEvent.StartDate;
            storedEvent.From = newEvent.From;
            storedEvent.To = newEvent.To;
            storedEvent.Country = newEvent.Country;
            storedEvent.Category = newEvent.Category;
            storedEvent.EventCategoryId = newEvent.EventCategoryId;

            try
            {
                _unitOfWork.Repository<Event>().Update(storedEvent);
                var result = await _unitOfWork.CompleteAsync();
                if (result <= 0) return null;

                return storedEvent;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }
    }
}
