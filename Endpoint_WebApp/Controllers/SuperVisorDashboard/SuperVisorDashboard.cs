using Endpoint_WebApp.Models.ViewModels.SuperVisorDashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nursing_Service.Application.Services.Nurse.Query.GetNurses;
using Nursing_Service.Application.Services.PatinetNeedService.Query.GetPatientNeedServices;
using Nursing_Service.Application.Services.SuperVisor.Command.AssignNurseToPNS;
using System.Security.Claims;

namespace Endpoint_WebApp.Controllers.SuperVisorDashboard
{
    [Authorize(Roles = "SuperVisor")]
    public class SuperVisorDashboard : Controller
    {
        private readonly IGetPatientNeedServices _getPatientNeedServices;
        private readonly IGetNursesService _getNursesService;
        private readonly IAssignNurseToPNS _assignNurseToPNS;

        public SuperVisorDashboard(
            IGetPatientNeedServices getPatientNeedServices,
            IGetNursesService getNursesService,
            IAssignNurseToPNS assignNurseToPNS)
        {
            _getPatientNeedServices = getPatientNeedServices;
            _getNursesService = getNursesService;
            _assignNurseToPNS = assignNurseToPNS;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> NursesList()
        {
            var nurseResult = await _getNursesService.ExcuteAsync();
            if (nurseResult.IsSuccess)
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                ulong? superVisorId = null;
                if (ulong.TryParse(userIdString, out var parsedId))
                    superVisorId = parsedId;

                var pns = nurseResult?.Data?.Where(n => n.SuperVisorId == superVisorId).ToList();

                return View(pns);
            }
            else
                return BadRequest(nurseResult.Message);
        }

        [HttpGet]
        public async Task<IActionResult> RequestList()
        {
            var pnsResult = await _getPatientNeedServices.ExcuteAsync();
            if (pnsResult.IsSuccess)
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                ulong? superVisorId = null;
                if (ulong.TryParse(userIdString, out var parsedId))
                    superVisorId = parsedId;

                var pns = pnsResult?.Data?.Where(pns => pns.SuperVisorId == superVisorId).ToList();

                return View(pns);
            }
            else
                return BadRequest(pnsResult.Message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignNurse([FromBody] AssignNurseViewModel dto)
        {
            if (dto == null || dto.RequestId == 0 || dto.NurseId == 0)
                return BadRequest("اطلاعات ارسالی نامعتبر است.");

            var result = await _assignNurseToPNS.ExcuteAsync(dto.RequestId, dto.NurseId);

            if (result.IsSuccess)
                return Ok(new { success = true, message = result.Message });
            else
                return BadRequest(result.Message);
        }
    }
}
