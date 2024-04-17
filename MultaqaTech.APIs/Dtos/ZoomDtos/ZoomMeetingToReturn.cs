namespace MultaqaTech.APIs.Dtos.ZoomDtos;

public class ZoomMeetingToReturn
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string AuthorName { get; set; }

    public string Content { get; set; }

    public string? PictureUrl { get; set; }

    public int Duration { get; set; }
    public int MeetingId { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }

    public string StartDate { get; set; }

}
