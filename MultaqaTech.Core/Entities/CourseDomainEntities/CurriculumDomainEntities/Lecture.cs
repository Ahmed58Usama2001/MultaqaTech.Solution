namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class Lecture:CurriculumItem
{
    public string VideoUrl
    {
        get { return base.MediaUrl; }
        set { base.MediaUrl = value; }
    }

    public CurriculumItemType CurriculumItemType { get; set; }=CurriculumItemType.Lecture;

    public List<Note>? Notes { get; set; } = new();
    public List<Question>? Questions { get; set; } = new();

}
