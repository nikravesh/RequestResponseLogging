using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace RequestResponseLogging.Middlewares;

public class RequestResponseLoggingMiddleware
{
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public RequestResponseLoggingMiddleware(ILogger<RequestResponseLoggingMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await LogRequestAsync(context.Request);
        Stream originalBodyStream = context.Response.Body;
        using MemoryStream responseBody = new MemoryStream();
        context.Response.Body = responseBody;
        await _next(context);
        await LogResponseAsync(context.Response);
        await responseBody.CopyToAsync(originalBodyStream);
    }

    #region Request
    private async Task LogRequestAsync(HttpRequest httpRequest)
    {
        httpRequest.EnableBuffering();
        StringBuilder sb = new();
        string formatRequest = await FormatRequest(httpRequest);
        sb.Append("Request: ").AppendLine(formatRequest);
        AddCustomRequestHeader(sb, httpRequest);
        _logger.LogInformation(sb.ToString());
    }

    private async Task<string> FormatRequest(HttpRequest request)
    {
        StreamReader reader = new(request.Body, Encoding.UTF8, false, -1, true);
        string body = await reader.ReadToEndAsync();

        return GetFormattedRequestString(request, body);
    }

    private string GetFormattedRequestString(HttpRequest request, string body)
    {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 5);
        defaultInterpolatedStringHandler.AppendFormatted(request.Scheme);
        defaultInterpolatedStringHandler.AppendLiteral(" ");
        defaultInterpolatedStringHandler.AppendFormatted(request.Host);
        defaultInterpolatedStringHandler.AppendFormatted(request.Path);
        defaultInterpolatedStringHandler.AppendLiteral(" ");
        defaultInterpolatedStringHandler.AppendFormatted(request.QueryString);
        defaultInterpolatedStringHandler.AppendLiteral(" ");
        defaultInterpolatedStringHandler.AppendFormatted(body);
        string formattedRequest2 = defaultInterpolatedStringHandler.ToStringAndClear();
        request.Body.Position = 0L;
        return formattedRequest2;
    }

    private void AddCustomRequestHeader(StringBuilder sb, HttpRequest request)
    {
        List<string> logHeaderKeys = new List<string> { "ApiGatewayCorrelationId", "UserId", "AccessToken" };
        sb.AppendLine("Request headers: ");
        IEnumerable<KeyValuePair<string, StringValues>> enumerable = from x in request.Headers.ToList()
                                                                     where logHeaderKeys.Any((string h) => h.ToLower() == x.Key.ToLower())
                                                                     select x;
        foreach (KeyValuePair<string, StringValues> item in enumerable)
        {
            sb.Append(item.Key).Append(':').AppendLine(item.Value);
        }

    }
    #endregion

    #region Response
    private async Task LogResponseAsync(HttpResponse httpResponse)
    {
        string response = await FormatResponse(httpResponse);
        StringBuilder builder = new StringBuilder();
        builder.Append("Response: ").AppendLine(response);
        _logger.LogInformation(builder.ToString());
    }

    private async Task<string> FormatResponse(HttpResponse httpResponse)
    {
        httpResponse.Body.Seek(0L, SeekOrigin.Begin);
        string response = await new StreamReader(httpResponse.Body).ReadToEndAsync();
        httpResponse.Body.Seek(0L, SeekOrigin.Begin);

        return GetFormattedResponseLog(httpResponse, response);
    }

    private string GetFormattedResponseLog(HttpResponse httpResponse, string response)
    {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
        defaultInterpolatedStringHandler.AppendFormatted(httpResponse.StatusCode);
        defaultInterpolatedStringHandler.AppendLiteral(": ");
        defaultInterpolatedStringHandler.AppendFormatted(response);

        return defaultInterpolatedStringHandler.ToStringAndClear();
    }
    #endregion
}
