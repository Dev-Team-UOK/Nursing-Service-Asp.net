namespace Nursing_Service.Application.Services.Patient.Command.CreatePatientNeedService
{
    public class CreatePatientRequesInfo
    {
        public DateTime ServiceDateTime { get; set; }
        public ulong SuperVisorId { get; set; }
        public ulong ServiceId { get; set; }
        public ulong PatientId { get; set; }
    }
}
