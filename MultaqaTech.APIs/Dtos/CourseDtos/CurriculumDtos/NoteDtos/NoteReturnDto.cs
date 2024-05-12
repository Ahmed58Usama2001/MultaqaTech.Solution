namespace MultaqaTech.APIs.Dtos.CourseDtos.CurriculumDtos.NoteDtos;

public class NoteReturnDto:NoteCreateDto
{
    public int Id { get; set; }
    public DateTime PublishingDate { get; set; }
}
