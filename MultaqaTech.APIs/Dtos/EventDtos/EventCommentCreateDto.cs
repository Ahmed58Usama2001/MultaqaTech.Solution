namespace MultaqaTech.APIs.Dtos.EventDtos
{
    public class EventCommentCreateDto
    {
        public string CommentContent { get; set; } = string.Empty;

        public int EventId { get; set; }
    }
}
