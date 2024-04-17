namespace MultaqaTech.Core.Entities.ZoomDomainEntites;

public class ZoomMeeting : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? AuthorName { get; set; }
    public int MeetingId { get; set; }

    public string Content { get; set; } = string.Empty;

    public string? PictureUrl { get; set; }
    public int ZoomMeetingCategoryId { get; set; }
    public ZoomMeetingCategory Category { get; set; }
    public DateTime StartDate { get; set; }
    public int Duration { get; set; }
}
