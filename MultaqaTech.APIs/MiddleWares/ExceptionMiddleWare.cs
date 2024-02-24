namespace MultaqaTech.APIs.MiddleWares;

public class ExceptionMiddleWare
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionMiddleWare> logger;
    private readonly IHostEnvironment env;

    public ExceptionMiddleWare(RequestDelegate next,ILogger<ExceptionMiddleWare> logger,IHostEnvironment env)
    {
        this.next = next;
        this.logger = logger;
        this.env = env;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next.Invoke(httpContext);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);


            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment() ?
                new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString()) :
                new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

            var options=new JsonSerializerOptions() {PropertyNamingPolicy=JsonNamingPolicy.CamelCase};

            var json=JsonSerializer.Serialize(response, options);

            await httpContext.Response.WriteAsync(json);
        }      
    
    }
}
