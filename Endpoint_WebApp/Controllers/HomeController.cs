using System.Diagnostics;
using Endpoint_WebApp.Models;
using Endpoint_WebApp.Models.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Nursing_Service.Application.Services.RequestForm.Command;

namespace Endpoint_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICreateRequestForm _createRequestForm;

        public HomeController(ILogger<HomeController> logger, ICreateRequestForm createRequestForm)
        {
            _logger = logger;
            _createRequestForm = createRequestForm;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateForm(CreateFormViewModel req)
        {
            var result = await _createRequestForm.ExcuteAsync(new CreateRequestFormRequestInfo
            {
                FullName = req.FullName,
                PhoneNumber = req.PhoneNumber,
                Address = req.Address,
                ServiceName = req.ServiceName
            });

            return Json(result);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
