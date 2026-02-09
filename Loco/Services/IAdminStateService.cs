using Microsoft.AspNetCore.Http;

namespace Loco.Web.Services
{
    // Single responsibility: tells if current user is admin (DEV cookie for now).
    public interface IAdminStateService
    {
        bool IsAdmin(HttpContext context);
    }
}