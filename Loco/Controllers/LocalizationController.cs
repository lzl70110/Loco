using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Loco.Web.Controllers
{
    public sealed class LocalizationController : Controller
    {
        [HttpPost]
        public IActionResult Set(string culture, string returnUrl = "/")
        {
            // Записва културата в cookie за 1 година
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return LocalRedirect(returnUrl);
        }
    }
}