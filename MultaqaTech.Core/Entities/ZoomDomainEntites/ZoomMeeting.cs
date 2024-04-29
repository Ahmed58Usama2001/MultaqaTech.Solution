namespace MultaqaTech.Core.Entities.ZoomDomainEntites;

public class ZoomMeeting : BaseEntityWithMediaUrl
{
    public string Title { get; set; } = string.Empty;
    public string? AuthorName { get; set; }
    public string MeetingId { get; set; }
    public string MeetingUrl { get; set; }
    public string TimeZone { get; set; }
    public string Content { get; set; } = string.Empty;

    public string ZoomPictureUrl
    {
        get { return base.MediaUrl; }
        set { base.MediaUrl = value; }
    }

    public int ZoomMeetingCategoryId { get; set; }
    public ZoomMeetingCategory Category { get; set; }

    public DateTime StartDate { get; set; }
    public int Duration { get; set; }
}
