using Microsoft.AspNetCore.Mvc;
using Nursing_Service.Application.Services.Service.Command.Create;
using Nursing_Service.Application.Services.Service.Command.Update;
using Nursing_Service.Application.Services.Service.Command.Delete;
using Nursing_Service.Application.Services.Service.Query.GetServices;

namespace Endpoint_WebApp.Controllers.Service
{
    [Route("Services")]
    public class ServiceController : Controller
    {
        private readonly IGetServices _getService;
        private readonly ICreateService _createService;
        private readonly IUpdateService _updateService;
        private readonly IDeleteService _deleteService;

        public ServiceController(
            IGetServices getService,
            ICreateService createService,
            IUpdateService updateService,
            IDeleteService deleteService)
        {
            _getService = getService;
            _createService = createService;
            _updateService = updateService;
            _deleteService = deleteService;
        }

        [HttpGet]
        [Route("View-Service")]
        public async Task<IActionResult> ViewService([FromQuery] ulong id)
        {
            var result = await _getService.ExcuteAsync();
            if (!result.IsSuccess || result.Data == null)
                return NotFound(result.Message);

            var service = result.Data.FirstOrDefault(s => s.Id == id);
            if (service == null)
                return NotFound();

            return View(service);
        }

        [HttpGet]
        [Route("Create-Service")]
        public IActionResult CreateService()
        {
            return View();
        }

        [HttpPost]
        [Route("Create-Service")]
        public async Task<IActionResult> CreateService(CreateServiceRequestInfo req)
        {
            var result = await _createService.ExcuteAsync(req);

            if (result.IsSuccess)
                return RedirectToAction("Services", "AdminDashboard");
            else
                return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("Edit-Service")]
        public async Task<IActionResult> EditService([FromQuery] ulong id)
        {
            var result = await _getService.ExcuteAsync();
            if (!result.IsSuccess || result.Data == null)
                return NotFound();

            var service = result.Data.FirstOrDefault(s => s.Id == id);
            if (service == null)
                return NotFound();

            var model = new UpdateServiceRequestInfo
            {
                Id = service.Id,
                Name = service.Name,
                Cost = service.Cost,
                MinDuration = service.MinDuration
                // سایر فیلدهای مورد نیاز را اضافه کنید
            };

            return View(model);
        }

        [HttpPost]
        [Route("Edit-Service")]
        public async Task<IActionResult> EditService(UpdateServiceRequestInfo req)
        {
            var result = await _updateService.ExcuteAsync(req);

            if (result.IsSuccess)
                return RedirectToAction("Services", "AdminDashboard");
            else
                return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("Delete-Service")]
        public async Task<IActionResult> DeleteService([FromQuery] ulong id)
        {
            var result = await _deleteService.ExcuteAsync(id);
            if (result.IsSuccess)
            {
                return RedirectToAction("Services", "AdminDashboard");
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
