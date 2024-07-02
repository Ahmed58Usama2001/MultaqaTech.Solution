namespace MultaqaTech.APIs.Dtos.EventDtos
{
    public class EventToReturnDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string AboutTheEvent { get; set; } = string.Empty;

        public string EventBy { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string Address { get; set; }
        public string? PictureUrl { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string Price { get; set; }
        public List<EventSpeakerToReturnDto>? Speakers { get; set; } = new();
        public List<EventCommentToReturnDto>? Comments { get; set; } = new();

    }
}
