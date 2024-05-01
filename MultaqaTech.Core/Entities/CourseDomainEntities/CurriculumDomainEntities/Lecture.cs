namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class Lecture : CurriculumItem
{
    public string VideoUrl
    {
        get { return base.MediaUrl; }
        set { base.MediaUrl = value; }
    }

    public override CurriculumItemType CurriculumItemType { get; set; } = CurriculumItemType.Lecture;

    [JsonIgnore]
    public List<Note>? Notes { get; set; } = new();
    [JsonIgnore]
    public List<Question>? Questions { get; set; } = new();

}
