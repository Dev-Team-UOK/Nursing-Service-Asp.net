using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint_WebApp.Controllers
{
    [Authorize(Roles = "Operator")]
    public class OperatorDashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
