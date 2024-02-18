namespace MultaqaTech.APIs.Extensions;

public static class IdentityServiceExtension
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped(typeof(IAuthService), typeof(AuthService));

        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.Password.RequiredUniqueChars = 2;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
        }).AddEntityFrameworkStores<AppIdentityDbContext>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                var secretKey = Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]);
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
                    ClockSkew = TimeSpan.FromDays(double.Parse(configuration["JWT:DurationInDays"]))
                };

            });


        return services;
    }

}
