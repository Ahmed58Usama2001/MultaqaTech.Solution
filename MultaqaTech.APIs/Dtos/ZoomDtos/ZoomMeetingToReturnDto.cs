namespace MultaqaTech.APIs.Dtos.ZoomDtos;

public class ZoomMeetingToReturnDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string AuthorName { get; set; }

    public string Content { get; set; }

    public string? PictureUrl { get; set; }

    public int Duration { get; set; }
    public string MeetingId { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }
    public string MeetingUrl { get; set; }
    public string TimeZone { get; set; }

    public string StartDate { get; set; }

}
