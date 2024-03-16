namespace MultaqaTech.APIs.Dtos;

public class CourseReviewToReturnDto
{
    public int Id { get; set; }
    public int NumberOfStars { get; set; }

    public DateTime Date { get; set; }

    public string? Content { get; set; }
    public string StudentName { get; set; }
    public string ProfilePictureUrl { get; set; }
}