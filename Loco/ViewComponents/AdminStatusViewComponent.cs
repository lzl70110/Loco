using Loco.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Loco.Web.ViewComponents
{
    // Renders the "Admin" label with green or light color based on admin state
    public class AdminStatusViewComponent : ViewComponent
    {
        private readonly IAdminStateService _adminState;

        public AdminStatusViewComponent(IAdminStateService adminState)
        {
            _adminState = adminState;
        }

        public IViewComponentResult Invoke()
        {
            bool isAdmin = _adminState.IsAdmin(HttpContext);
            return View(isAdmin); // passes bool model to the view
        }
    }
}