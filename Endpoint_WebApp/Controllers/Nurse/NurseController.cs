using Microsoft.AspNetCore.Mvc;
using Nursing_Service.Application.Services.Nurse.Command.Create;
using Nursing_Service.Application.Services.Nurse.Command.Delete;
using Nursing_Service.Application.Services.Nurse.Command.Update;
using Nursing_Service.Application.Services.Nurse.Query.GetNurses;
using Nursing_Service.Domain.Entities.Nurse;

namespace Endpoint_WebApp.Controllers.Nurse
{
    [Route("Nurses")]
    public class NurseController : Controller
    {
        private readonly IGetNursesService _getNurse;
        private readonly ICreateNurseService _createNurse;
        private readonly IDeleteNurseService _deleteNurse;
        private readonly IUpdateNurseService _updateNurse;

        public NurseController(
            IGetNursesService getNurse,
            ICreateNurseService createNurse,
            IDeleteNurseService deleteNurse,
            IUpdateNurseService updateNurse)
        {
            _getNurse = getNurse;
            _createNurse = createNurse;
            _deleteNurse = deleteNurse;
            _updateNurse = updateNurse;
        }

        [HttpGet]
        [Route("View-Nurse")]
        public async Task<IActionResult> ViewNurse([FromQuery] ulong id)
        {
            var nurseResult = await _getNurse.ExcuteAsync(id);
            if (nurseResult.IsSuccess is not true)
            {
                return NotFound(nurseResult.Message);
            }
            else
            {
                return View(nurseResult.Data.First());
            }
        }

        [HttpGet]
        [Route("Create-Nurse")]
        public IActionResult CreateNurse()
        {
            return View();
        }

        [HttpPost]
        [Route("Create-Nurse")]
        public async Task<IActionResult> CreateNurse(CreateNurseRequestInfo req)
        {
            var result = await _createNurse.ExcuteAsync(req);

            if (result.IsSuccess)
                return RedirectToAction("Nurses", "AdminDashboard");
            else
                return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("Edit-Nurse")]
        public async Task<IActionResult> EditNurse([FromQuery] ulong id)
        {
            var nurseResult = await _getNurse.ExcuteAsync(id);
            if (!nurseResult.IsSuccess || nurseResult.Data == null)
                return NotFound();

            var nurse = nurseResult.Data.First();
            var model = new UpdateNurseRequestInfo
            {
                Id = nurse.Id,
                Password = nurse.Password,
                UserName = nurse.UserName,
                PhoneNumber = nurse.PhoneNumber,
                LastName = nurse.LastName,
                Email = nurse.Email,
                DoService = nurse.DoService.Select(ds => ds.Id).ToList(),
                FirstName = nurse.FirstName,
                SuperVisorId = nurse.SuperVisorId,
                WorkHistoryInYear = nurse.WorkHistoryInYear
            };

            return View(model);
        }

        [HttpPost]
        [Route("Edit-Nurse")]
        public async Task<IActionResult> EditNurse(UpdateNurseRequestInfo req)
        {
            var result = await _updateNurse.ExcuteAsync(req);

            if (result.IsSuccess)
                return RedirectToAction("Nurses", "AdminDashboard");
            else
                return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("Delete-Nurse")]
        public async Task<IActionResult> DeleteNurse([FromQuery] ulong id)
        {
            var result = await _deleteNurse.ExcuteAsync(id);
            if (result.IsSuccess)
            {
                return RedirectToAction("Nurses", "AdminDashboard");
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}