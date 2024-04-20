namespace MultaqaTech.Core.Entities.Identity;

public class AppUser : IdentityUser
{
    public string? Bio { get; set; }
    public string? JobTitle { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;

    public DateTime RegistrationDate { get; set; }

    public bool IsInstructor { get; set; }
}