namespace MultaqaTech.APIs.Dtos.AccountDtos;

public class BecomeInstructorDto
{
    public string Bio { get; set; }
    public string JobTitle { get; set; }

    public IFormFile? ProfilePicture { get; set; }
}
