using Endpoint_WebApp.Models.ViewModels.AdminDashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nursing_Service.Application.Services.Nurse.Query.GetNurses;
using Nursing_Service.Application.Services.Patient.Query.GetPatients;
using Nursing_Service.Application.Services.PatinetNeedService.Query.GetPatientNeedServices;
using Nursing_Service.Application.Services.Service.Query.GetServices;
using Nursing_Service.Application.Services.SuperVisor.Query.GetSuperVisors;
using Nursing_Service.Application.Services.Users.Query;

namespace Endpoint_WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {
        private readonly IGetPatients _getPatients;
        private readonly IGetUserService _getUser;
        private readonly IGetNursesService _getNurses;
        private readonly IGetPatientNeedServices _getPatientNeedServices;
        private readonly IGetServices _getServices;
        private readonly IGetSuperVisors _getSuperVisorService;

        public AdminDashboardController(IGetPatients getPatients, IGetUserService getUser, IGetNursesService getNurses, IGetPatientNeedServices getPatientNeedServices, IGetServices getServices, IGetSuperVisors getSuperVisorService)
        {
            _getPatients = getPatients;
            _getUser = getUser;
            _getNurses = getNurses;
            _getPatientNeedServices = getPatientNeedServices;
            _getServices = getServices;
            _getSuperVisorService = getSuperVisorService;
        }

        public async Task<IActionResult> Reports()
        {
            ViewData["PageTitle"] = "گزارشات";

            var nurseResult = await _getNurses.ExcuteAsync();
            ulong? numberOfNurses = 0;
            if (nurseResult.IsSuccess is true)
                numberOfNurses = (ulong)nurseResult?.Data?.Count;

            var patientsResult = await _getPatients.ExcuteAsync();
            ulong? numberOfPatients = 0;
            if (patientsResult.IsSuccess is true)
                numberOfPatients = (ulong)patientsResult?.Data?.Count;

            var servicesResult = await _getServices.ExcuteAsync();
            ulong? numberOfServices = 0;
            if (servicesResult.IsSuccess is true)
                numberOfServices = (ulong)servicesResult?.Data?.Count;

            var pnssResult = await _getPatientNeedServices.ExcuteAsync();
            ulong? numberOfPatientNeedServicesInMonth = 0;
            if (pnssResult.IsSuccess is true)
                numberOfPatientNeedServicesInMonth = (ulong)(pnssResult.Data?.Where(pns => pns.ServiceDateTime.Month == DateTime.Now.Month).Count());

            return View(new ReportsViewModel
            {
                NumberOfNurses = numberOfNurses,
                NumberOfPatients = numberOfPatients,
                NumberOfPNSInThisMonth = numberOfPatientNeedServicesInMonth,
                NumberOfServices = numberOfServices
            });
        }

        public async Task<IActionResult> Operators()
        {
            var operators = await _getUser.ExcuteAsync();

            ViewData["PageTitle"] = "اپراتورها";

            return View(operators.Data);
        }

        public async Task<IActionResult> NeedServices()
        {
            var pnss = await _getPatientNeedServices.ExcuteAsync();

            ViewData["PageTitle"] = "لیست درخواست‌ها";

            return View(pnss.Data);
        }

        public async Task<IActionResult> Nurses()
        {
            var nurses = await _getNurses.ExcuteAsync();

            ViewData["PageTitle"] = "پرستاران";

            return View(nurses.Data);
        }

        public async Task<IActionResult> Patients()
        {
            var patients = await _getPatients.ExcuteAsync();

            ViewData["PageTitle"] = "بیماران";

            return View(patients.Data);
        }

        public async Task<IActionResult> Services()
        {
            var services = await _getServices.ExcuteAsync();

            ViewData["PageTitle"] = "مدیریت سرویس‌ها";
            
            return View(services.Data);
        }

        public async Task<IActionResult> SuperVisors()
        {
            var supervisors = await _getSuperVisorService.ExcuteAsync();

            ViewData["PageTitle"] = "سوپروایزرها";

            return View(supervisors.Data);
        }
    }
}
