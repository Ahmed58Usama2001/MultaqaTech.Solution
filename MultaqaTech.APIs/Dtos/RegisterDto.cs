namespace MultaqaTech.APIs.Dtos;

public class RegisterDto
{
    [Required]
    [MinLength(3, ErrorMessage = "The FirstName must be at least 3 characters long.")]
    [MaxLength(100, ErrorMessage = "The FirstName must be less than 100 characters long.")]
    public string FirstName { get; set; }

    [Required]
    [MinLength(3, ErrorMessage = "The LastName must be at least 3 characters long.")]
    [MaxLength(100, ErrorMessage = "The LastName must be less than 100 characters long.")]
    public string LastName { get; set; }

    [Required]
    [MinLength(3, ErrorMessage = "The UserName must be at least 3 characters long.")]
    [MaxLength(100, ErrorMessage = "The UserName must be less than 100 characters long.")]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"^(?:\+?20|0)?1\d{9}$", ErrorMessage = "Invalid Egyptian phone number.")]
    public string PhoneNumber { get; set; }

    [Required]
    public string Password { get; set; }
}
