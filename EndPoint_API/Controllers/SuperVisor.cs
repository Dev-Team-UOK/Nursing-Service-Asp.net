using Microsoft.AspNetCore.Mvc;
using Nursing_Service.Application.Interfaces.Contexts;

namespace EndPoint_API.Controllers
{
    [ApiController]
    [Route("Api/SuperVisor")]
    public class SuperVisorController : ControllerBase
    {
        private readonly IDataBaseContext _context;
        public SuperVisorController(IDataBaseContext context)
        {
            _context = context;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var supervisors = _context.SuperVisors
                .Select(s => new
                {
                    s.Id,
                    s.UserName,
                    s.FirstName,
                    s.LastName,
                    FullName = s.FirstName + s.LastName ,
                })
                .ToList();

            return Ok(supervisors);
        }
    }
}
