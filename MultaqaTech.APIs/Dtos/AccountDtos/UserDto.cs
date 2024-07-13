namespace MultaqaTech.APIs.Dtos.AccountDtos;

public class UserDto
{
    public string UserName { get; set; }

    public string? ProfilePictureUrl { get; set; }
    public string? JobTitle { get; set; }
    public string? Bio { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }
    public DateTime RegistrationDate { get; set; }

    public bool IsInstructor { get; set; }
    public int InstructorId { get; set; }
    public int StudentId { get; set; }

    public string Email { get; set; }

    public string Token { get; set; }

}
