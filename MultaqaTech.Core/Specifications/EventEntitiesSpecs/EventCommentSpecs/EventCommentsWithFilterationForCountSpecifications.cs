
namespace MultaqaTech.Core.Specifications.EventEntitiesSpecs.EventCommentSpecs
{
    public class EventCommentsWithFilterationForCountSpecifications : BaseSpecifications<EventComment>
    {
        public EventCommentsWithFilterationForCountSpecifications(EventCommentSpeceficationsParams speceficationsParams) :
             base(p => (!speceficationsParams.EventId.HasValue || p.EventId == speceficationsParams.EventId.Value))


        {
        
        }
    }
}
