namespace MultaqaTech.APIs.Dtos.ZoomDtos
{
    public class CreateMeetingRequestDto
    {
        public string topic { get; set; }
        public DateTime start_time { get; set; }
        public int duration { get; set; }
    }
}
