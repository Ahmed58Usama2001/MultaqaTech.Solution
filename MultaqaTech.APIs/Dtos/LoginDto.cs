namespace MultaqaTech.APIs.Dtos;

public class LoginDto
{
    [Required]
    public string UserNameOrEmail { get; set; }

    [Required]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}
