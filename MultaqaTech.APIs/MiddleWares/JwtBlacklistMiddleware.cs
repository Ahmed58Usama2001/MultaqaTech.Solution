using Newtonsoft.Json;

namespace MultaqaTech.APIs.MiddleWares;

public class JwtBlacklistMiddleware
{
        private readonly RequestDelegate _next;
        private readonly IConnectionMultiplexer _redis;

        public JwtBlacklistMiddleware(RequestDelegate next, IConnectionMultiplexer redis)
        {
            _next = next;
           _redis = redis;
        }

        public async Task Invoke(HttpContext context, IUnitOfWork unitOfWork)
        {
            // Check if the request contains authorization header
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token != null)
                {
                    var redis = _redis.GetDatabase();
                    var key = $"blacklisted_token:{token}";

                    var isBlacklisted = await redis.KeyExistsAsync(key);

                    if (isBlacklisted)  // Use either blacklistedToken.HasValue or isBlacklisted
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json"; // Set content type

                        var errorResponse = new { message = "The provided token is invalid or blacklisted." };

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
                        return;
                    }
                }

            await _next(context);
        }
    
}
