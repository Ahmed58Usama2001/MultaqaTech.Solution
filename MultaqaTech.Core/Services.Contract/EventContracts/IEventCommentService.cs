
namespace MultaqaTech.Core.Services.Contract.EventContracts
{
    public interface IEventCommentService
    {
        Task<EventComment?> CreateEventAsync(EventComment eventComment);

        Task<IReadOnlyList<EventComment>> ReadAllEventCommentsAsync(EventCommentSpeceficationsParams speceficationsParams);

        Task<EventComment?> ReadByIdAsync(int eventCommentId);

        Task<EventComment?> UpdateEventComment(int eventCommentId, EventComment updatedEventComment);

        Task<bool> DeleteEventComment(EventComment eventComment);

        Task<int> GetCountAsync(EventCommentSpeceficationsParams speceficationsParams);
    }
}
