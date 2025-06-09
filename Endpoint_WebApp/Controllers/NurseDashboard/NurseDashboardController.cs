using Endpoint_WebApp.Models.ViewModels.NurseDashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nursing_Service.Application.Services.Nurse.Command.CreateAbility;
using Nursing_Service.Application.Services.PatinetNeedService.Query.GetPatientNeedServices;
using Nursing_Service.Application.Services.Service.Query.GetServices;
using System.Security.Claims;

namespace Endpoint_WebApp.Controllers.NurseDashboard
{
    [Authorize(Roles = "Nurse")]
    public class NurseDashboardController : Controller
    {
        private readonly IGetServices _getServices;
        private readonly IGetPatientNeedServices _getPatientNeedService;
        private readonly ICreateAbility _createAbilityService;

        public NurseDashboardController(IGetServices getServices, IGetPatientNeedServices getPatientNeedService, ICreateAbility createAbilityService)
        {
            _getServices = getServices;
            _getPatientNeedService = getPatientNeedService;
            _createAbilityService = createAbilityService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Ability()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ulong? nurseId = null;
            if (ulong.TryParse(userIdString, out var parsedId))
                nurseId = parsedId;

            var result = await _getServices.ExcuteAsync(nurseId: nurseId);
            if (result.IsSuccess)
                return View(result.Data);
            else
                return BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAbility()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbility(CreateAbilityViewModel req)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ulong? nurseId = null;
            if (ulong.TryParse(userIdString, out var parsedId))
                nurseId = parsedId;

            var result = await _createAbilityService.ExcuteAsync(nurseId!.Value, req.ServiceId);

            if (result.IsSuccess)
                return RedirectToAction(nameof(Ability));
            else
                return BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> RequestList()
        {
            var pns = await _getPatientNeedService.ExcuteAsync();

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ulong? nurseId = null;
            if (ulong.TryParse(userIdString, out var parsedId))
                nurseId = parsedId;

            if (pns.IsSuccess)
            {
                pns.Data = pns.Data?.Where(pns => pns.NurseId == nurseId).ToList();
                return View(pns.Data);
            }
            else
            {
                return BadRequest(pns.Message);
            }
        }


    }
}
