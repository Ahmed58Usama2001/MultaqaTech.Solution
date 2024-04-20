namespace MultaqaTech.Core.Entities.Identity;

public class AppUser : IdentityUser
{
    public string? ProfilePictureUrl { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;

    public DateTime RegistrationDate { get; set; }
}