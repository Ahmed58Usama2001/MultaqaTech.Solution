namespace MultaqaTech.Core.Entities.CourseDomainEntities;

public class CoursePrerequist
{
    public int  CourseId { get; set; }
    public Course Course { get; set; }

    public int  PrerequistId { get; set; }
    public Subject Prerequist { get; set; }

    public string PrerequistName { get; set; }
}