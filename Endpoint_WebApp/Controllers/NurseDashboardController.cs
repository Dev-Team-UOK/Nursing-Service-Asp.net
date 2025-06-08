using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint_WebApp.Controllers
{
    [Authorize(Roles = "Nurse")]
    public class NurseDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
