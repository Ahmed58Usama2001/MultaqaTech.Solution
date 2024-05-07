
namespace MultaqaTech.Core.Entities.EventDomainEntities
{
    public class EventComment : BaseEntity
    {
        public string CommentContent { get; set; }

        public string? AuthorName { get; set; }

        public DateTime DatePosted { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
