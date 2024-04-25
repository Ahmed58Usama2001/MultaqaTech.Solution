namespace MultaqaTech.Core.Entities.Identity;

public class AppUser : IdentityUser
{
    public string? ProfilePictureUrl { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;

    public int? StudentId { get; set; } 
    public Student? Student { get; set; } 

    public int? InstructorId { get; set; } 
    public Instructor? Instructor { get; set; } 

    public DateTime RegistrationDate { get; set; }
}