namespace MultaqaTech.APIs.MiddleWares;

public class RateLimeterMiddleware(RequestDelegate next)
{
    private static int counter = 0;
    private static DateTime lastRequestTime = DateTime.Now;

    private readonly RequestDelegate _next = next;
    public async Task Invoke(HttpContext context)
    {
        counter++;
        if (DateTime.Now.Second - lastRequestTime.Second >= 5)
        {
            counter = 1;
            await _next(context);
            lastRequestTime = DateTime.Now;
        }
        else
        {
            if (counter < 10)
            {
                lastRequestTime = DateTime.Now;
                await _next(context);
            }
            else
            {
                lastRequestTime = DateTime.Now;
                await context.Response.WriteAsync("Request Limit Exceded");
            }
        }
    }
}
