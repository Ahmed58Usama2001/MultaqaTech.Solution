
namespace MultaqaTech.Service.EventEntitesServices
{
    public class EventCommentService(IUnitOfWork unitOfWork) : IEventCommentService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<EventComment?> CreateEventAsync(EventComment eventComment)
        {
            try
            {
                await _unitOfWork.Repository<EventComment>().AddAsync(eventComment);
                var result = await _unitOfWork.CompleteAsync();
                if (result <= 0) return null;

                return eventComment;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }

        public async Task<bool> DeleteEventComment(EventComment eventComment)
        {
            try
            {
                _unitOfWork.Repository<EventComment>().Delete(eventComment);

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

        public async Task<int> GetCountAsync(EventCommentSpeceficationsParams speceficationsParams)
        {
            var countSpec = new EventCommentsWithFilterationForCountSpecifications(speceficationsParams);

            var count = await _unitOfWork.Repository<EventComment>().GetCountAsync(countSpec);

            return count;
        }

        public async Task<IReadOnlyList<EventComment>> ReadAllEventCommentsAsync(EventCommentSpeceficationsParams speceficationsParams)
        {
            var spec = new EventCommentWithIncludesSpecifications(speceficationsParams);

            var eventComments = await _unitOfWork.Repository<EventComment>().GetAllWithSpecAsync(spec);

            return eventComments;
        }

        public async Task<EventComment?> ReadByIdAsync(int eventCommentId)
        {
            var spec = new EventCommentWithIncludesSpecifications(eventCommentId);

            var eventComment = await _unitOfWork.Repository<EventComment>().GetByIdWithSpecAsync(spec);

            return eventComment;
        }

        public async Task<EventComment?> UpdateEventComment(int eventCommentId, EventComment updatedEventComment)
        {
            var eventComment = await _unitOfWork.Repository<EventComment>().GetByIdAsync(eventCommentId);
            if (eventComment == null) return null;

            if (updatedEventComment == null || string.IsNullOrWhiteSpace(updatedEventComment.CommentContent))
                return null;

            eventComment = updatedEventComment;

            try
            {
                _unitOfWork.Repository<EventComment>().Update(eventComment);
                var result = await _unitOfWork.CompleteAsync();
                if (result <= 0) return null;

                return eventComment;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
        }
    }
}
