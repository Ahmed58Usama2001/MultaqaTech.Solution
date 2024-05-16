
namespace MultaqaTech.Service.EventEntitesServices
{
    public class EventSpeakerService(IUnitOfWork unitOfWork) : IEventSpeakerService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<EventSpeaker?> CreateEventSpeakerAsync(EventSpeaker eventSpeaker)
        {
            try
            {
                await _unitOfWork.Repository<EventSpeaker>().AddAsync(eventSpeaker);
                var result = await _unitOfWork.CompleteAsync();
                if (result <= 0) return null;

                return eventSpeaker;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }

        public async Task<bool> DeleteEventSpeaker(EventSpeaker eventSpeaker)
        {
            try
            {
                _unitOfWork.Repository<EventSpeaker>().Delete(eventSpeaker);

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

        public async Task<IReadOnlyList<EventSpeaker>> ReadAllEventSpeakersAsync()
        {
            try
            {
                var eventSpeakers = await _unitOfWork.Repository<EventSpeaker>().GetAllAsync();

                return eventSpeakers;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }

        public async Task<EventSpeaker?> ReadByIdAsync(int eventSpeakerId)
        {
            try
            {
                var eventSpeaker = await _unitOfWork.Repository<EventSpeaker>().GetByIdAsync(eventSpeakerId);

                return eventSpeaker;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }

        public async Task<EventSpeaker?> UpdateEventSpeaker(int eventSpeakerId, EventSpeaker updatedEventSpeaker)
        {
            var eventSpeaker = await _unitOfWork.Repository<EventSpeaker>().GetByIdAsync(eventSpeakerId);

            if (eventSpeaker == null) return null;

            if (updatedEventSpeaker == null || string.IsNullOrWhiteSpace(updatedEventSpeaker.Name))
                return null;

            eventSpeaker.Name = updatedEventSpeaker.Name;
            eventSpeaker.JobTitle = updatedEventSpeaker.JobTitle;
            eventSpeaker.SpeakerPictureUrl = updatedEventSpeaker.SpeakerPictureUrl;

            try
            {
                _unitOfWork.Repository<EventSpeaker>().Update(eventSpeaker);
                var result = await _unitOfWork.CompleteAsync();
                if (result <= 0) return null;

                return eventSpeaker;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }
    }
}
