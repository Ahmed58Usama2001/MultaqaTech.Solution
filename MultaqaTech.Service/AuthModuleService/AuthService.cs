namespace MultaqaTech.Service.AuthModuleService;

public class AuthService(IConfiguration configuration, IConnectionMultiplexer redis,
        IGoogleAuthService googleAuthService,
        IFacebookAuthService facebookAuthService,
        UserManager<AppUser> userManager,
        MultaqaTechContext context) : IAuthService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IConnectionMultiplexer _redis = redis;
    private readonly IGoogleAuthService _googleAuthService = googleAuthService;
    private readonly IFacebookAuthService _facebookAuthService = facebookAuthService;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly MultaqaTechContext _context = context;


    public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
    {
        // Private claims (user-defined)
        var authClaims = new List<Claim>()
        {
           new Claim(ClaimTypes.GivenName, user?.UserName??string.Empty),
            new Claim(ClaimTypes.Email, user?.Email??string.Empty)
        };

        var userRoles = await userManager.GetRolesAsync(user);

        foreach (var role in userRoles)
            authClaims.Add(new Claim(ClaimTypes.Role, role));

        var secretKey = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"] ?? string.Empty);
        var requiredKeyLength = 256 / 8; // 256 bits
        if (secretKey.Length < requiredKeyLength)
        {
            // Pad the key to meet the required length
            Array.Resize(ref secretKey, requiredKeyLength);
        }

        var token = new JwtSecurityToken(
            audience: _configuration["JWT:ValidAudience"],
            issuer: _configuration["JWT:ValidIssuer"],
            expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"] ?? "0")),
            claims: authClaims,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<bool> InvalidateSignedInTokenAsync(string token)
    {
        try
        {
            var redis = _redis.GetDatabase(); // Access the Redis database using dependency injection

            var key = $"blacklisted_token:{token}"; // Use a descriptive key format
            var expiration = TimeSpan.FromDays(1); // Set expiration for 1 day

            var added = await redis.StringSetAsync(key, string.Empty, expiration, (When)CommandFlags.None);

            return added;
        }
        catch (Exception ex)
        {
            Log.Error(ex,ex.ToString());
            return false;
        }
    }

    public async Task<AppUser> SignInWithFacebook(FacebookSignInVM model)
    {
        FacebookTokenValidationResponse? validatedFbToken = await _facebookAuthService.ValidateFacebookToken(model.AccessToken);

        if (validatedFbToken is null)
            return new AppUser();


        FacebookUserInfoResponse? userInfo = await _facebookAuthService.GetFacebookUserInformation(model.AccessToken);

        if (userInfo is null)
            return new AppUser();


        var userToBeCreated = new CreateUserFromSocialLogin
        {
            UserName = userInfo.Name,
            Email = userInfo.Email,
            ProfilePicture = userInfo.Picture.Data.Url.AbsoluteUri,
            LoginProviderSubject = userInfo.Id,
        };

        var user = await _userManager.CreateUserFromSocialLogin(_context, userToBeCreated, LoginProvider.Facebook);

        if (user is null)
            return new AppUser();

        return user;
    }

    public async Task<AppUser> SignInWithGoogle(GoogleSignInVM model)
    {
        var response = await _googleAuthService.GoogleSignIn(model);

        if (response is null)
            return new AppUser();

        return response;
    }
}