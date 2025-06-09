using System.ComponentModel;

namespace Nursing_Service.Application.Services.PatinetNeedService.Command.CreatePatientNeedService
{
    public class CreatePatientNeedServiceRequesInfo
    {
        [DisplayName("تاریخ و زمان انجام سرویس")]
        public DateTime ServiceDateTime { get; set; }
        [DisplayName("سوپروایزر")]
        public ulong SuperVisorId { get; set; }
        [DisplayName("سرویس")]
        public ulong ServiceId { get; set; }
        [DisplayName("بیمار")]
        public ulong PatientId { get; set; }
        [DisplayName("پرستار")]
        public ulong? NurseId { get; set; }
    }
}
