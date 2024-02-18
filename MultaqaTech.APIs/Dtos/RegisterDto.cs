namespace MultaqaTech.APIs.Dtos;

public class RegisterDto
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public bool AcceptTerms { get; set; }
}
