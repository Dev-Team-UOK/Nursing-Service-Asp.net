using Microsoft.AspNetCore.Mvc;
using Nursing_Service.Application.Services.Users.Commands.Create;
using Nursing_Service.Application.Services.Users.Commands.Delete;
using Nursing_Service.Application.Services.Users.Commands.Update;
using Nursing_Service.Application.Services.Users.Query;

namespace Endpoint_WebApp.Controllers.Operator
{
    [Route("Operators")]
    public class OperatorController : Controller
    {
        private readonly IGetUserService _getOperator;
        private readonly ICreateUserService _createOperator;
        private readonly IDeleteUserService _deleteOperator;
        private readonly IUpdateUserService _updateOperator;

        public OperatorController(
            IGetUserService getOperator,
            ICreateUserService createOperator,
            IDeleteUserService deleteOperator,
            IUpdateUserService updateOperator)
        {
            _getOperator = getOperator;
            _createOperator = createOperator;
            _deleteOperator = deleteOperator;
            _updateOperator = updateOperator;
        }

        [HttpGet]
        [Route("View-Operator")]
        public async Task<IActionResult> ViewOperator([FromQuery] ulong id)
        {
            var operatorResult = await _getOperator.ExcuteAsync(id);
            if (operatorResult.IsSuccess is not true)
            {
                return NotFound(operatorResult.Message);
            }
            else
            {
                return View(operatorResult.Data!.First());
            }
        }

        [HttpGet]
        [Route("Create-Operator")]
        public IActionResult CreateOperator()
        {
            return View();
        }

        [HttpPost]
        [Route("Create-Operator")]
        public async Task<IActionResult> CreateOperator(CreateUserRequestInfo req)
        {
            var result = await _createOperator.ExcuteAsync(req);

            if (result.IsSuccess)
                return RedirectToAction("Operators", "AdminDashboard");
            else
                return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("Edit-Operator")]
        public async Task<IActionResult> EditOperator([FromQuery] ulong id)
        {
            var operatorResult = await _getOperator.ExcuteAsync(id);
            if (!operatorResult.IsSuccess || operatorResult.Data == null)
                return NotFound();

            var _operator = operatorResult.Data.First(); 

            var model = new UpdateUserRequestInfo
            {
                Id = _operator.Id,
                FirstName = _operator.FirstName,
                Email = _operator.Email,
                LastName = _operator.LastName,
                PhoneNumber = _operator.PhoneNumber,
                UserName = _operator.UserName,
                Password = _operator.Password
            };

            return View(model);
        }

        [HttpPost]
        [Route("Edit-Operator")]
        public async Task<IActionResult> EditOperator(UpdateUserRequestInfo req)
        {
            var result = await _updateOperator.ExcuteAsync(req);

            if (result.IsSuccess)
                return RedirectToAction("Operators", "AdminDashboard");
            else
                return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("Delete-Operator")]
        public async Task<IActionResult> DeleteOperator([FromQuery] ulong id)
        {
            var result = await _deleteOperator.ExcuteAsync(id);
            if (result.IsSuccess)
            {
                return RedirectToAction("Operators", "AdminDashboard");
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}