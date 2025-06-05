namespace Nursing_Service.Application.Services.PatinetNeedService.Command.UpdatePatientNeedService
{
    public class UpdatePatientNeedServiceRequestInfo
    {
        public ulong Id { get; set; }
        public DateTime? ServiceDateTime { get; set; }
        public ulong? SuperVisorId { get; set; }
        public ulong? ServiceId { get; set; }
    }
}
