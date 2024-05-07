namespace MultaqaTech.APIs.Dtos.EventDtos
{
    public class EventCommentToReturnDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }

        public string? AuthorName { get; set; }
        public string Event { get; set; } = string.Empty;
        public string DatePosted { get; set; } = string.Empty;
        public string CommentContent { get; set; } = string.Empty;
    }
}
