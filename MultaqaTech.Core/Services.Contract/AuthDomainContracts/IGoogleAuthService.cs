using DesignsAndBuild.Core.Entities.Identity.Gmail;

namespace MultaqaTech.Core.Services.Contract.AccountModuleContracts;

public interface IGoogleAuthService
{
    Task<AppUser> GoogleSignIn(GoogleSignInVM model);
}
