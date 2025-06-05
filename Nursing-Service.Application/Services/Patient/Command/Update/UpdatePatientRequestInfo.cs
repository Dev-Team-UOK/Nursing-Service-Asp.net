namespace Nursing_Service.Application.Services.Patient.Command.Update
{
    public class UpdatePatientRequestInfo
    {
        public ulong Id { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public uint? Age { get; set; } 
        public uint? Height { get; set; }
        public uint? Weight { get; set; }
        public string? PhoneNumber { get; set; }
        public string? IllnessHistory { get; set; }
    }
}
