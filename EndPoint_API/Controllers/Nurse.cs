using Microsoft.AspNetCore.Mvc;
using Nursing_Service.Application.Interfaces.Contexts;

namespace EndPoint_API.Controllers
{
    [ApiController]
    [Route("api/nurse")]
    public class NurseController : ControllerBase
    {
        private readonly IDataBaseContext _context;
        public NurseController(IDataBaseContext context)
        {
            _context = context;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var nurses = _context.Nurses
                .Select(n => new
                {
                    n.Id,
                    n.UserName,
                    n.FirstName,
                    n.LastName,
                    FullName = (n.FirstName ?? "") + " " + (n.LastName ?? "")
                })
                .ToList();

            return Ok(nurses);
        }
    }
}
