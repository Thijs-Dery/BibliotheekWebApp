using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using BibliotheekApp.Models;

public class VisitorLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public VisitorLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, BibliotheekContext dbContext)
    {
        var log = new VisitorLog
        {
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            Url = context.Request.Path,
            Method = context.Request.Method,
            Timestamp = DateTime.UtcNow
        };

        // Sla de log op in de database
        dbContext.VisitorLogs.Add(log);
        await dbContext.SaveChangesAsync();

        // Ga verder met de pipeline
        await _next(context);
    }
}
