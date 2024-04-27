using RequestResponseLogging.Middlewares;

namespace RequestResponseLogging.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UserRequestResponseLogging(this IApplicationBuilder builder) =>
       builder.UseMiddleware<RequestResponseLoggingMiddleware>(Array.Empty<object>());

    public static IApplicationBuilder UseUnHandleException(this IApplicationBuilder builder) =>
        builder.UseMiddleware<UnhandleExceptionMiddleware>(Array.Empty<object>());
}
