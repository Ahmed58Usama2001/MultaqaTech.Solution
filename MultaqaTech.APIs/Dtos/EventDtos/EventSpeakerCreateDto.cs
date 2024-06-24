namespace MultaqaTech.APIs.Dtos.EventDtos
{
    public class EventSpeakerCreateDto
    {

        [MaxLength(100, ErrorMessage = "The Speaker Name must be less than 100 characters long.")]
        [MinLength(3, ErrorMessage = "The Speaker Name must be at least 3 characters long.")]
        public string Name { get; set; }

        public string JobTitle { get; set; }

        public IFormFile PictureUrl { get; set; }
        public int EventId { get; set; }
    }
}
