using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint_WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {
        public IActionResult NeedServices()
        {
            return View();
        }

        public IActionResult Nurses()
        {
            return View();
        }

        public IActionResult Patients()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult SuperVisors()
        {
            return View();
        }
    }
}
