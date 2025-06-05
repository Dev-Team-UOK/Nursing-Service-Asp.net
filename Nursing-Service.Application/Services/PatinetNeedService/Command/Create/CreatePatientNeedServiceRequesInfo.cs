namespace Nursing_Service.Application.Services.PatinetNeedService.Command.CreatePatientNeedService
{
    public class CreatePatientNeedServiceRequesInfo
    {
        public DateTime ServiceDateTime { get; set; }
        public ulong SuperVisorId { get; set; }
        public ulong ServiceId { get; set; }
        public ulong PatientId { get; set; }
    }
}
