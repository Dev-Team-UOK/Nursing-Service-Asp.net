using Endpoint_WebApp.Models.ViewModels.AdminDashboard;
using Microsoft.AspNetCore.Mvc;
using Nursing_Service.Application.Services.Patient.Command.Create;
using Nursing_Service.Application.Services.Patient.Command.Delete;
using Nursing_Service.Application.Services.Patient.Command.Update;
using Nursing_Service.Application.Services.Patient.Query.GetPatient;

namespace Endpoint_WebApp.Controllers.Patient
{
    [Route("Patients")]
    public class PatientController : Controller
    {
        private readonly IGetPatientService _getPatient;
        private readonly ICreatePatientService _createPatient;
        private readonly IDeletePatient _deletePatient;
        private readonly IUpdatePatientService _updatePatient;

        public PatientController(IGetPatientService getPatient, ICreatePatientService createPatient, IDeletePatient deletePatient, IUpdatePatientService updatePatient)
        {
            _getPatient = getPatient;
            _createPatient = createPatient;
            _deletePatient = deletePatient;
            _updatePatient = updatePatient;
        }

        [HttpGet]
        [Route("View-Patient")]
        public async Task<IActionResult> ViewPatient([FromQuery]ulong id)
        {
            var patient = await _getPatient.ExcuteAsync(id);
            if (patient.IsSuccess is not true)
            {
                return NotFound(patient.Message);
            }
            else
            {
                return View(patient.Data);
            }
                
        }
        
        [HttpGet]
        [Route("Create-Patient")]
        public IActionResult CreatePatient()
        {
            return View();
        }

        [HttpPost]
        [Route("Create-Patient")]
        public async Task<IActionResult> CreatePatient(CreatePatinetRequestInfo req)
        {
            var result = await _createPatient.ExcuteAsync(req);

            if (result.IsSuccess)
                return RedirectToAction("Patients", "AdminDashboard");
            else
                return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("Edit-Patient")]
        public async Task<IActionResult> EditPatient([FromQuery]ulong id) 
        {
            var patientResult = await _getPatient.ExcuteAsync(id);
            if (!patientResult.IsSuccess || patientResult.Data == null)
                return NotFound();

            // تبدیل مدل به مدل ویرایش (در صورت نیاز)
            var model = new UpdatePatientRequestInfo
            {
                Id = patientResult.Data.Id,
                FullName = patientResult.Data.FullName,
                Age = patientResult.Data.Age,
                Height = patientResult.Data.Height,
                Weight = patientResult.Data.Weight,
                PhoneNumber = patientResult.Data.PhoneNumber,
                Address = patientResult.Data.Address,
                IllnessHistory = patientResult.Data.IllnessHistory
            };

            return View(model);
        }

        [HttpPost]
        [Route("Edit-Patient")]
        public async Task<IActionResult> EditPatient(UpdatePatientRequestInfo req)
        {
            var result = await _updatePatient.ExcuteAsync(req);

            if (result.IsSuccess)
                return RedirectToAction("Patients", "AdminDashboard");
            else
                return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("Delete-Patient")]
        public async Task<IActionResult> DeletePatient([FromQuery]ulong Id)
        {
            var result = await _deletePatient.ExcuteAsync(Id);
            if (result.IsSuccess)
            {
                return RedirectToAction("Patients", "AdminDashboard");
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
