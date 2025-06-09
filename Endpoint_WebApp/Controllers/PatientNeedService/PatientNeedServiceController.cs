using Microsoft.AspNetCore.Mvc;
using Nursing_Service.Application.Services.PatinetNeedService.Command.CreatePatientNeedService;
using Nursing_Service.Application.Services.PatinetNeedService.Command.DeletePatientNeedService;
using Nursing_Service.Application.Services.PatinetNeedService.Command.UpdatePatientNeedService;
using Nursing_Service.Application.Services.PatinetNeedService.Query.GetPatientNeedServices;

namespace Endpoint_WebApp.Controllers.PatientNeedService
{
    [Route("PatientNeedServices")]
    public class PatientNeedServiceController : Controller
    {
        private readonly IGetPatientNeedServices _getPatientNeedService;
        private readonly ICreatePatientNeedService _createPatientNeedService;
        private readonly IUpdatePatientNeedService _updatePatientNeedService;
        private readonly IDeletePatientNeedService _deletePatientNeedService;

        public PatientNeedServiceController(
            IGetPatientNeedServices getPatientNeedService,
            ICreatePatientNeedService createPatientNeedService,
            IUpdatePatientNeedService updatePatientNeedService,
            IDeletePatientNeedService deletePatientNeedService)
        {
            _getPatientNeedService = getPatientNeedService;
            _createPatientNeedService = createPatientNeedService;
            _updatePatientNeedService = updatePatientNeedService;
            _deletePatientNeedService = deletePatientNeedService;
        }

        [HttpGet]
        [Route("View-PatientNeedService")]
        public async Task<IActionResult> ViewPatientNeedService([FromQuery] ulong id)
        {
            var result = await _getPatientNeedService.ExcuteAsync();
            if (!result.IsSuccess || result.Data == null)
                return NotFound(result.Message);

            var patientNeedService = result.Data.FirstOrDefault(p => p.Id == id);
            if (patientNeedService == null)
                return NotFound();

            return View(patientNeedService);
        }

        [HttpGet]
        [Route("Create-PatientNeedService")]
        public IActionResult CreatePatientNeedService()
        {
            return View();
        }

        [HttpPost]
        [Route("Create-PatientNeedService")]
        public async Task<IActionResult> CreatePatientNeedService(CreatePatientNeedServiceRequesInfo req)
        {
            var result = await _createPatientNeedService.ExcuteAsync(req);

            if (result.IsSuccess)
                return RedirectToAction("NeedServices", "AdminDashboard");
            else
                return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("Edit-PatientNeedService")]
        public async Task<IActionResult> EditPatientNeedService([FromQuery] ulong id)
        {
            var result = await _getPatientNeedService.ExcuteAsync();
            if (!result.IsSuccess || result.Data == null)
                return NotFound();

            var patientNeedService = result.Data.FirstOrDefault(p => p.Id == id);
            if (patientNeedService == null)
                return NotFound();

            var model = new UpdatePatientNeedServiceRequestInfo
            {
                Id = patientNeedService.Id,
                // سایر فیلدهای مورد نیاز را اینجا مقداردهی کنید
            };

            return View(model);
        }

        [HttpPost]
        [Route("Edit-PatientNeedService")]
        public async Task<IActionResult> EditPatientNeedService(UpdatePatientNeedServiceRequestInfo req)
        {
            var result = await _updatePatientNeedService.ExcuteAsync(req);

            if (result.IsSuccess)
                return RedirectToAction("NeedServices", "AdminDashboard");
            else
                return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("Delete-PatientNeedService")]
        public async Task<IActionResult> DeletePatientNeedService([FromQuery] ulong id)
        {
            var result = await _deletePatientNeedService.ExcuteAsync(id);
            if (result.IsSuccess)
            {
                return RedirectToAction("NeedServices", "AdminDashboard");
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
