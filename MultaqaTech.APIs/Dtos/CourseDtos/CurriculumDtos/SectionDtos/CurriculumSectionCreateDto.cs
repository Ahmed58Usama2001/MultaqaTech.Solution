namespace MultaqaTech.APIs.Dtos.CourseDtos.CurriculumDtos.SectionDtos;

public class CurriculumSectionCreateDto
{
    public string Title { get; set; }

    public string? Objectives { get; set; }

    public int CourseId { get; set; }
}
