namespace MultaqaTech.APIs.Dtos.EventDtos
{
    public class EventSpeakerToReturnDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; }

        public string JobTitle { get; set; }

        public string Event { get; set; } = string.Empty;

        public string? PictureUrl { get; set; }
    }
}
