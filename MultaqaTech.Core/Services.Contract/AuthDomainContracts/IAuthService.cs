using DesignsAndBuild.Core.Entities.Identity.Gmail;
using DesignsAndBuild.Core.Entities.Identity;

namespace MultaqaTech.Core.Services.Contract.AuthDomainContracts;

public interface IAuthService
{
    Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);
    Task<bool> InvalidateSignedInTokenAsync(string token);

    Task<JwtResponseVM> SignInWithGoogle(GoogleSignInVM model);
    Task<JwtResponseVM> SignInWithFacebook(FacebookSignInVM model);


}
