using Nursing_Service.Application.Services.Users.Commands.Create;
using System.ComponentModel;

namespace Nursing_Service.Application.Services.Nurse.Command.Create
{
    public class CreateNurseRequestInfo : CreateUserRequestInfo
    {
        [DisplayName("شماره پرستاری")]
        public required string NurseNumber { get; set; }
        [DisplayName("سوپروایزر")]
        public ulong? SuperVisorId { get; set; }
        [DisplayName("سابقه کاری(سال)")]
        public short? WorkHistoryInYear { get; set; }
        [DisplayName("تاریخ شروع همکاری")]
        public DateTime StartWorkingInCompany { get; set; }
        [DisplayName("توانایی ها")]
        public required List<ulong> DoService { get; set; }
    }
}
