using Nursing_Service.Application.Services.Users.Commands.Create;
using Nursing_Service.Application.Services.Users.Commands.Update;
using System.ComponentModel;

namespace Nursing_Service.Application.Services.Nurse.Command.Update
{
    public class UpdateNurseRequestInfo : UpdateUserRequestInfo
    {
        public ulong? SuperVisorId { get; set; }
        [DisplayName("سابقه کاری(سال)")]
        public short? WorkHistoryInYear { get; set; }
        public List<ulong> DoService { get; set; }
    }
}
