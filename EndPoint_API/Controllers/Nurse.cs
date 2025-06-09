using Microsoft.AspNetCore.Mvc;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Domain.Entities.Nurse;

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
        public IActionResult GetAll([FromQuery]ulong? superVisorId = null)
        {
            if (superVisorId is not null)
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
            else
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
}
