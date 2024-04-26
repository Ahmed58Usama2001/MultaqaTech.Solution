namespace MultaqaTech.Service.AuthModuleService;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly MultaqaTechContext _context;
    private readonly GoogleAuthConfig _googleAuthConfig;


    public GoogleAuthService(
        UserManager<AppUser> userManager,
        MultaqaTechContext context,
        IOptions<GoogleAuthConfig> googleAuthConfig
        )
    {
        _userManager = userManager;
        _context = context;
        _googleAuthConfig = googleAuthConfig.Value;
    }

    public async Task<AppUser> GoogleSignIn(GoogleSignInVM model)
    {

        Payload payload = new();

        try
        {
            payload = await ValidateAsync(model.IdToken, new ValidationSettings
            {
                Audience = new[] { model.ClientId }
            });

        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }

        var userToBeCreated = new CreateUserFromSocialLogin
        {
            UserName = payload.GivenName,
            Email = payload.Email,
            ProfilePicture = payload.Picture,
            LoginProviderSubject = payload.Subject,
        };

        var user = await _userManager.CreateUserFromSocialLogin(_context, userToBeCreated, LoginProvider.Google);

        if (user is not null)
            return user;

        else
            return null;
    }
}