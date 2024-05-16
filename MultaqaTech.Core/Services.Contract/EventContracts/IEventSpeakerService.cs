
namespace MultaqaTech.Core.Services.Contract.EventContracts
{
    public interface IEventSpeakerService
    {
        Task<EventSpeaker?> CreateEventSpeakerAsync(EventSpeaker eventSpeaker);

        Task<IReadOnlyList<EventSpeaker>> ReadAllEventSpeakersAsync();

        Task<EventSpeaker?> ReadByIdAsync(int eventSpeakerId);

        Task<EventSpeaker?> UpdateEventSpeaker(int eventSpeakerId, EventSpeaker updatedEventSpeaker);

        Task<bool> DeleteEventSpeaker(EventSpeaker eventSpeaker);

    }
}
