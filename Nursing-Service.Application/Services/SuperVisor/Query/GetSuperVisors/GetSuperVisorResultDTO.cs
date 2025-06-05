using Nursing_Service.Application.Services.Nurse.Query.GetNurses;
using Nursing_Service.Application.Services.Users.Query;
using Nursing_Service.Domain.Entities.SuperVisor;

namespace Nursing_Service.Application.Services.SuperVisor.Query.GetSuperVisors
{
    public class GetSuperVisorResultDTO : GetUserResultDTO
    {
        public Shift Shift { get; set; }
        public List<GetNurseResultDTO>? Nurses { get; set; }
    }
}
