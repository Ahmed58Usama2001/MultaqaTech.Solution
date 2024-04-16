﻿namespace MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities;

public class Answer:BaseEntity
{
    public string Description { get; set; }

    public string AnswererId { get; set; }

    public DateTime PublishingDate { get; set; } = DateTime.Now;

    public int QuestionId { get; set; }
    public Question Question { get; set; }
}
