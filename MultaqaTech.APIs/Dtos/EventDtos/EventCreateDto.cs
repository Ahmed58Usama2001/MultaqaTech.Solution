namespace MultaqaTech.APIs.Dtos.EventDtos
{
    public class EventCreateDto
    {
        public int CategoryId { get; set; }
        public int CountryId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string AboutTheEvent { get; set; } = string.Empty;

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string Address { get; set; }
        public IFormFile PictureUrl { get; set; }
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Website { get; set; }
        public string Price { get; set; }
      //  public List<int>? Speakers { get; set; } = [];
    }
}
