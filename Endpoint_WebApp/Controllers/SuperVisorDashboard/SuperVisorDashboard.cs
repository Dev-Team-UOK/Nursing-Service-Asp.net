using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint_WebApp.Controllers.SuperVisorDashboard
{
    [Authorize(Roles = "SuperVisor")]
    public class SuperVisorDashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
