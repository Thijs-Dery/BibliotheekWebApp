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
        // Controleer de taal in de querystring
        string cultureQuery = context.Request.Query["culture"];

        // Controleer of er een cookie beschikbaar is
        string cultureCookie = context.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];

        // Gebruik querystring, cookie, of standaardwaarde
        string cultureName = cultureQuery ??
                     CookieRequestCultureProvider.ParseCookieValue(cultureCookie)?.Cultures[0].Value ??
                     "nl"; // Standaard naar Nederlands


        // Stel de cultuur in
        CultureInfo culture = new CultureInfo(cultureName);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        // Bewaar de cultuur in een cookie als deze uit de querystring komt
        if (!string.IsNullOrEmpty(cultureQuery))
        {
            context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultureName)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(30) }
            );
        }

        // Ga verder naar de volgende middleware
        await _next(context);
    }
}
