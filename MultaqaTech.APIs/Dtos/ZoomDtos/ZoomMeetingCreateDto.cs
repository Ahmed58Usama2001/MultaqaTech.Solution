namespace MultaqaTech.APIs.Dtos.ZoomDtos;

public class ZoomMeetingCreateDto
{
    public string Title { get; set; }

    public string Content { get; set; }
    public DateTime StartDate { get; set; }
    public int Duration { get; set; }

    public string? PictureUrl { get; set; }
    public string TimeZone { get; set; }

    public int CategoryId { get; set; }
}
