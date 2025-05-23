namespace Nursing_Service.Application.Services.Patient.Query.GetPatientNeedServices
{
    public class GetPatientNeedServicesResultDTO
    {
        public DateTime ServiceDateTime { get; set; }
        public bool IsDone { get; set; } = false;
        public bool IsPast { get; set; } = false;
        public ulong SuperVisorId { get; set; }
        public ulong? NurseId { get; set; }
        public ulong? ServiceId { get; set; }
        public ulong PatientId { get; set; }
    }
}
