using Nursing_Service.Application.Services.Service.Query.GetServices;
using Nursing_Service.Application.Services.Users.Query;

namespace Nursing_Service.Application.Services.Nurse.Query.GetNurses
{
    public class GetNurseResultDTO : GetUserResultDTO
    {
        public ulong Id { get; set; }
        public required string NurseNumber { get; set; }
        public ulong? SuperVisorId { get; set; }
        public string SuperVisorFullName { get; set; }
        public short? WorkHistoryInYear { get; set; }
        public DateTime StartWorkingInCompany { get; set; }
        public List<GetServiceResultDTO> DoService { get; set; }
    }
}
