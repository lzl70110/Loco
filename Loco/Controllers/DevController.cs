using Microsoft.AspNetCore.Mvc;

namespace Loco.Web.Controllers;

public sealed class DevController : Controller
{
    private const string CookieName = "Dev-IsAdmin";

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Toggle(bool isAdmin, string returnUrl = "/")
    {
        Response.Cookies.Append(
            CookieName,
            isAdmin ? "1" : "0",
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                IsEssential = true,
                Path = "/",
                HttpOnly = false,   // dev-only
                Secure = false,     // set true if HTTPS-only
                SameSite = SameSiteMode.Lax
            });

        return LocalRedirect(returnUrl);
    }
}