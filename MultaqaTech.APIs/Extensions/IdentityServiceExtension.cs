namespace MultaqaTech.APIs.Extensions;

public static class IdentityServiceExtension
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped(typeof(IAuthService), typeof(AuthService));

        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
        }).AddEntityFrameworkStores<AppIdentityDbContext>()
          .AddDefaultTokenProviders()
          .AddRoles<IdentityRole>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                var secretKey = Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"] ?? string.Empty);
                var requiredKeyLength = 256 / 8; // 256 bits
                if (secretKey.Length < requiredKeyLength)
                {
                    // Pad the key to meet the required length
                    Array.Resize(ref secretKey, requiredKeyLength);
                }

                // Configure authentication handler
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromDays(double.Parse(configuration["JWT:DurationInDays"] ?? string.Empty))
                };

            }).AddGoogle(options =>
            {
                IConfigurationSection googleAuthSection = configuration.GetSection("Authentication:Google");

                options.ClientId = googleAuthSection["ClientId"];
                options.ClientSecret = googleAuthSection["ClientSecret"];
            }).AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
            });


        return services;
    }

}
