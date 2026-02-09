using Microsoft.AspNetCore.Mvc;

namespace Loco.Web.Controllers;
public class LocomotivesController : Controller
    {
    public IActionResult Index()
        {
        return Ok();
        }
    }
