using StackExchange.Redis;

namespace MultaqaTech.APIs;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Configure services


        builder.Services.AddControllers();

        builder.Services.AddSwaggerServices();

        builder.Services.AddDbContext<MultaqaTechContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddDbContext<AppIdentityDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
        {
            var connection = builder.Configuration.GetConnectionString("Redis");
            return ConnectionMultiplexer.Connect(connection);
        });

        builder.Services.AddApplicationServices();

        builder.Services.AddIdentityServices(builder.Configuration);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MyPolicy", options =>
            {
                options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });
        });

        #endregion


        var app = builder.Build();

        using var scope = app.Services.CreateScope();

        var services = scope.ServiceProvider;

        var _dbContext = services.GetRequiredService<MultaqaTechContext>();
        ////Ask CLR for creating object from Context explicitly

        var _identityDbContext = services.GetRequiredService<AppIdentityDbContext>();


        var loggerFactory = services.GetRequiredService<ILoggerFactory>();

        try
        {    
            await _dbContext.Database.MigrateAsync(); // Update-Database
            //await MultaqaTechContextSeed.SeedAsync(_dbContext); // Data Seeding

            await _identityDbContext.Database.MigrateAsync(); // Update-Database

            var _userManager = services.GetRequiredService<UserManager<AppUser>>();
            await AppIdentityDbContextSeed.SeedUsersAsync(_userManager); // Data Seeding
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occurred during applying the migration.");
        }


        #region Configure Middlewares

        app.UseMiddleware<ExceptionMiddleWare>();
        app.UseMiddleware<JwtBlacklistMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerMiddlewares();
        }

        //app.UseMiddleware<RateLimeterMiddleware>();

        app.UseStatusCodePagesWithRedirects("/errors/{0}");

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseCors("MyPolicy");

        app.MapControllers();

        app.UseAuthentication();

        app.UseAuthorization();

        #endregion

        app.Run();
    }
}
