using Nursing_Service.Application.Services.Users.Commands.Create;
using Nursing_Service.Application.Services.Users.Commands.Update;

namespace Nursing_Service.Application.Services.Nurse.Command.Update
{
    public class UpdateNurseRequestInfo : UpdateUserRequestInfo
    {
        public ulong Id { get; set; }
        public ulong? SuperVisorId { get; set; }
        public short? WorkHistoryInYear { get; set; }
        public List<ulong> DoService { get; set; }
    }
}
