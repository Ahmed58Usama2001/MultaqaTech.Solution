namespace MultaqaTech.APIs.Dtos.CourseDtos.CurriculumDtos.ItemDtos;

public class ItemCreateDto
{
    public string Title { get; set; }

    public string? Description { get; set; }

    public int CurriculumSectionId { get; set; }
}
