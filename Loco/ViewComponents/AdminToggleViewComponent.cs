using Loco.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Loco.Web.ViewComponents
{
    // DEV-only: renders Admin ON/OFF buttons driven by DevController.Toggle(...)
    public sealed class AdminToggleViewComponent : ViewComponent
    {
        private readonly IAdminStateService _adminState;

        public AdminToggleViewComponent(IAdminStateService adminState)
        {
            _adminState = adminState;
        }

        public IViewComponentResult Invoke()
        {
            // Read current admin state from DEV cookie via service
            bool isAdmin = _adminState.IsAdmin(HttpContext);
            return View(isAdmin); // pass bool to the view
        }
    }
}