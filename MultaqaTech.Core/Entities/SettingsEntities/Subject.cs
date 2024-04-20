namespace MultaqaTech.Core.Entities.SettingsEntities;

public class Subject : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public List<BlogPost> BlogPosts { get; set; } = [];
    [JsonIgnore]
    public List<Course>? AssociatedCourses { get; set; } = [];
    [JsonIgnore]
    public List<ZoomMeeting> ZoomMeetings { get; set; } = [];
}