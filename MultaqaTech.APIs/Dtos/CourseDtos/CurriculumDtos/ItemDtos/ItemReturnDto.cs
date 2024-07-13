namespace MultaqaTech.APIs.Dtos.CourseDtos.CurriculumDtos.ItemDtos;

public class ItemReturnDto:ItemCreateDto
{
    public int Id { get; set; }
    public int Order { get; set; }
    public string ItemType { get; set; }

    public bool IsCompleted { get; set; }
}
