using Newtonsoft.Json;

namespace RequestResponseLogging.Middlewares;

public class UnhandleExceptionMiddleware
{
    private readonly ILogger<UnhandleExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;
    public UnhandleExceptionMiddleware(ILogger<UnhandleExceptionMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            string response = GetMessageForResponse(httpContext, ex);
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }

    private string GetMessageForResponse(HttpContext httpContext, Exception ex)
    {
        _logger.LogError(ex, "Unknown error occurs!");

        string response = JsonConvert.SerializeObject(new
        { isSuccess = false, errorCode = 500, message = "UnhandleExceptionMessage" });
        httpContext.Response.StatusCode = 500;
        httpContext.Response.ContentType = "application/json";

        return response;
    }
}
