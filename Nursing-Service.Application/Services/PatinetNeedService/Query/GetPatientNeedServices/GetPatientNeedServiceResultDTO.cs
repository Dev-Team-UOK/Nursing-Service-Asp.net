namespace Nursing_Service.Application.Services.PatinetNeedService.Query.GetPatientNeedServices
{
    public class GetPatientNeedServiceResultDTO
    {
        public ulong Id { get; set; }
        public DateTime ServiceDateTime { get; set; }
        public bool IsDone { get; set; } = false;
        public bool IsPast { get; set; } = false;
        public ulong SuperVisorId { get; set; }
        public required string SuperVisorFullName { get; set; }
        public ulong? NurseId { get; set; }
        public string NurseFullName { get; set; }
        public ulong? ServiceId { get; set; }
        public string ServiceName { get; set; }
        public ulong PatientId { get; set; }
        public required string PatientFullName { get; set; }
    }
}
