namespace MultaqaTech.Core.Entities;

public class Subject : BaseEntity
{
    public string Name { get; set; }

    [JsonIgnore]
    public List<BlogPost> BlogPosts { get; set; } = new();
}