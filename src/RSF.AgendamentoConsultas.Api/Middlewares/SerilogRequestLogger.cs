using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;

using Serilog;

namespace RSF.AgendamentoConsultas.Api.Middlewares;

[ExcludeFromCodeCoverage]
public class SerilogRequestLogger(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext, nameof(httpContext));

        //if (!Log.IsEnabled(Serilog.Events.LogEventLevel.Debug))
        //{
        //    await _next(httpContext);
        //    return;
        //}

        string requestBody = "";
        httpContext.Request.EnableBuffering();
        Stream body = httpContext.Request.Body;
        byte[] buffer = new byte[Convert.ToInt32(httpContext.Request.ContentLength)];
#pragma warning disable CA1835 // Prefer the 'Memory'-based overloads for 'ReadAsync' and 'WriteAsync'
#pragma warning disable CA2022 // A synchronous operation was disallowed. Call ReadAsync or set AllowSynchronousIO to true instead.
        await httpContext.Request.Body.ReadAsync(buffer, 0, buffer.Length);
#pragma warning restore CA1835 // Prefer the 'Memory'-based overloads for 'ReadAsync' and 'WriteAsync'
#pragma warning restore CA2022 // A synchronous operation was disallowed. Call ReadAsync or set AllowSynchronousIO to true instead.
        requestBody = Encoding.UTF8.GetString(buffer);
        body.Seek(0, SeekOrigin.Begin);
        httpContext.Request.Body = body;

        Log.ForContext("RequestHeaders", httpContext.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true);

        if (!string.IsNullOrEmpty(requestBody))
        {
            Log.ForContext("RequestBody", requestBody)
                .Information("[{Timestamp:dd/MM/yyyy HH:mm:ss} {Level}] HTTP Request {RequestMethod} {RequestPath}", DateTime.Now, "INF", httpContext.Request.Method, httpContext.Request.Path);
            Log.ForContext("RequestBody", requestBody)
                .Information("[{Timestamp:dd/MM/yyyy HH:mm:ss} {Level}] HTTP Request Body: {RequestBody}", DateTime.Now, "INF", requestBody);
        }

        using var responseBodyMemoryStream = new MemoryStream();
        var originalResponseBodyReference = httpContext.Response.Body;
        httpContext.Response.Body = responseBodyMemoryStream;

        var startTime = DateTime.Now;
        await _next(httpContext);

        httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();
        httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

        var elapsedMilliseconds = (DateTime.Now - startTime).TotalMilliseconds;

        if (!string.IsNullOrEmpty(responseBody))
        {
            string responseBodyJsonInline;

            try
            {
                var parsedJson = JsonDocument.Parse(responseBody);
                responseBodyJsonInline = JsonSerializer.Serialize(parsedJson.RootElement, new JsonSerializerOptions { WriteIndented = false });
            }
            catch
            {
                // Caso não seja um JSON válido, mantém o body como texto bruto
                responseBodyJsonInline = responseBody;
            }

            Log.ForContext("ResponseBody", responseBodyJsonInline)
                .Information("[{Timestamp:dd/MM/yyyy HH:mm:ss} {Level}] HTTP Response {RequestMethod} {RequestPath} responded {StatusCode} in {ElapsedMilliseconds} ms", DateTime.Now, "INF", httpContext.Request.Method, httpContext.Request.Path, httpContext.Response.StatusCode, elapsedMilliseconds);
            Log.ForContext("ResponseBody", responseBodyJsonInline)
                .Information("[{Timestamp:dd/MM/yyyy HH:mm:ss} {Level}] HTTP Response Body: {ResponseBody}", DateTime.Now, "INF", responseBodyJsonInline);
        }


        await responseBodyMemoryStream.CopyToAsync(originalResponseBodyReference);
    }
}

//  HTTP POST /api/account/login responded 200 in 7264.1662 ms