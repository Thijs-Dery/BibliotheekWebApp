using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Threading.Tasks;

public class CustomCultureMiddleware
{
    private readonly RequestDelegate _next;

    public CustomCultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string cultureQuery = context.Request.Query["culture"];
        string cultureCookie = context.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];

        string cultureName = cultureQuery ??
                     CookieRequestCultureProvider.ParseCookieValue(cultureCookie)?.Cultures[0].Value ??
                     "nl";

        CultureInfo culture = new CultureInfo(cultureName);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        if (!string.IsNullOrEmpty(cultureQuery))
        {
            context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultureName)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(30) }
            );
        }

        await _next(context);
    }
}
