
namespace MultaqaTech.Core.Services.Contract.AuthDomainContracts;

public interface IAuthService
{
    Task<string> CreateTokenAsync(Entities.Identity.AppUser user, UserManager<Entities.Identity.AppUser> userManager);
    Task<bool> InvalidateSignedInTokenAsync(string token);

    Task<JwtResponseVM?> SignInWithGoogle(GoogleSignInVM model);
    Task<JwtResponseVM?> SignInWithFacebook(FacebookSignInVM model);


}
