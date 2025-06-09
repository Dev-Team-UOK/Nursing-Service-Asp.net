using Microsoft.AspNetCore.Mvc;
using Nursing_Service.Application.Interfaces.Contexts;

namespace EndPoint_API.Controllers
{
    [ApiController]
    [Route("api/patient")]
    public class PatientController : ControllerBase
    {
        private readonly IDataBaseContext _context;
        public PatientController(IDataBaseContext context)
        {
            _context = context;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var patients = _context.Patients
                .Select(p => new
                {
                    p.Id,
                    p.FullName,
                    p.PhoneNumber
                })
                .ToList();

            return Ok(patients);
        }
    }
}
