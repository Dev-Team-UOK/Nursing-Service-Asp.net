using Nursing_Service.Application.Services.Users.Commands.Create;

namespace Nursing_Service.Application.Services.Nurse.Command.Create
{
    public class CreateNurseRequestInfo : CreateUserRequestInfo
    {
        public string NurseNumber { get; set; }
        public ulong? SuperVisorId { get; set; }
        public short? WorkHistoryInYear { get; set; }
        public DateTime StartWorkingInCompany { get; set; }
        public List<ulong> DoService { get; set; }
    }
}
