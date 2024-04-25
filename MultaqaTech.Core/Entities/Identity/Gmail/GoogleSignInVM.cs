namespace MultaqaTech.Core.Entities.Identity.Gmail;

public class GoogleSignInVM
{
    [Required]
    public string IdToken { get; set; }
    public string ClientId { get; set; }
}
