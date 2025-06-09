using Microsoft.AspNetCore.Mvc;
using Nursing_Service.Application.Services.SuperVisor.Command.Create;
using Nursing_Service.Application.Services.SuperVisor.Command.Delete;
using Nursing_Service.Application.Services.SuperVisor.Command.Update;
using Nursing_Service.Application.Services.SuperVisor.Query.GetSuperVisors;

namespace Endpoint_WebApp.Controllers.SuperVisor
{
    [Route("SuperVisors")]
    public class SuperVisorController : Controller
    {
        private readonly IGetSuperVisors _getSuperVisor;
        private readonly ICreateSuperVisor _createSuperVisor;
        private readonly IDeleteSuperVisor _deleteSuperVisor;
        private readonly IUpdateSuperVisor _updateSuperVisor;

        public SuperVisorController(
            IGetSuperVisors getSuperVisor,
            ICreateSuperVisor createSuperVisor,
            IDeleteSuperVisor deleteSuperVisor,
            IUpdateSuperVisor updateSuperVisor)
        {
            _getSuperVisor = getSuperVisor;
            _createSuperVisor = createSuperVisor;
            _deleteSuperVisor = deleteSuperVisor;
            _updateSuperVisor = updateSuperVisor;
        }

        [HttpGet]
        [Route("View-SuperVisor")]
        public async Task<IActionResult> ViewSuperVisor([FromQuery] ulong id)
        {
            var result = await _getSuperVisor.ExcuteAsync(id);
            if (!result.IsSuccess || result.Data == null)
                return NotFound(result.Message);

            return View(result.Data.First());
        }

        [HttpGet]
        [Route("Create-SuperVisor")]
        public IActionResult CreateSuperVisor()
        {
            return View();
        }

        [HttpPost]
        [Route("Create-SuperVisor")]
        public async Task<IActionResult> CreateSuperVisor(CreateSuperVisorRequestInfo req)
        {
            var result = await _createSuperVisor.ExcuteAsync(req);

            if (result.IsSuccess)
                return RedirectToAction("SuperVisors", "AdminDashboard");
            else
                return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("Edit-SuperVisor")]
        public async Task<IActionResult> EditSuperVisor([FromQuery] ulong id)
        {
            var result = await _getSuperVisor.ExcuteAsync(id);
            if (!result.IsSuccess || result.Data == null)
                return NotFound();

            var superVisor = result.Data.First();
            var model = new UpdateSuperVisorRequestInfo
            {
                Id = superVisor.Id,
                UserName = superVisor.UserName,
                FirstName = superVisor.FirstName,
                LastName = superVisor.LastName,
                PhoneNumber = superVisor.PhoneNumber,
                Email = superVisor.Email,
                Password = superVisor.Password,
                Shift = superVisor.Shift
                // سایر فیلدهای مورد نیاز را اضافه کنید
            };

            return View(model);
        }

        [HttpPost]
        [Route("Edit-SuperVisor")]
        public async Task<IActionResult> EditSuperVisor(UpdateSuperVisorRequestInfo req)
        {
            var result = await _updateSuperVisor.ExcuteAsync(req);

            if (result.IsSuccess)
                return RedirectToAction("SuperVisors", "AdminDashboard");
            else
                return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("Delete-SuperVisor")]
        public async Task<IActionResult> DeleteSuperVisor([FromQuery] ulong id)
        {
            var result = await _deleteSuperVisor.ExcuteAsync(id);
            if (result.IsSuccess)
            {
                return RedirectToAction("SuperVisors", "AdminDashboard");
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
