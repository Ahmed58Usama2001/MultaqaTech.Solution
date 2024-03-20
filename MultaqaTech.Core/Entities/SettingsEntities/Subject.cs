using MultaqaTech.Core.Entities.ZoomDomainEntites;

namespace MultaqaTech.Core.Entities.SettingsEntities;

public class Subject : BaseEntity
{
    public string Name { get; set; }

    [JsonIgnore]
    public List<BlogPost> BlogPosts { get; set; } = new();
    [JsonIgnore]
    public List<Course>? AssociatedCourses { get; set; } = new();
    [JsonIgnore]
    public List<ZoomMeeting> ZoomMeetings { get; set; } = new();
}