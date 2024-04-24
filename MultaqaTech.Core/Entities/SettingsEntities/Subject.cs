namespace MultaqaTech.Core.Entities.SettingsEntities;

public class Subject : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public List<BlogPost> BlogPosts { get; set; } = [];

    [JsonIgnore]
    public List<Course> AssociatedCoursesTags { get; set; } = new();
}