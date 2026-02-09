using Microsoft.AspNetCore.Http;

namespace Loco.Web.Services
{
    // DEV implementation: reads cookie "Dev-IsAdmin" set by DevController.Toggle(...)
    public sealed class AdminStateService : IAdminStateService
    {
        private const string CookieName = "Dev-IsAdmin";

        public bool IsAdmin(HttpContext context)
        {
            // Admin when the cookie value is "1"
            return context.Request.Cookies[CookieName] == "1";
        }
    }
}