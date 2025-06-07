using Microsoft.AspNetCore.Mvc;
using Nursing_Service.Application.Interfaces.Contexts;

namespace EndPoint_API.Controllers
{
    [ApiController]
    [Route("api/service")]
    public class ServiceController : ControllerBase
    {
        private readonly IDataBaseContext _context;
        public ServiceController(IDataBaseContext context)
        {
            _context = context;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var services = _context.Services.Select(s => new { s.Id, s.Name }).ToList();
            return Ok(services);
        }
    }

}
