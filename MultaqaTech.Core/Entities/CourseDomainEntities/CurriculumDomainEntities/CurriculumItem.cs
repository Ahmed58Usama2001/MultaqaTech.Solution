﻿namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public abstract class CurriculumItem:BaseEntityWithMediaUrl
{
    public string Title { get; set; }

    public string? Description { get; set; }

    public int CurriculumSectionId { get; set; }
    public CurriculumSection CurriculumSection { get; set; }
}
