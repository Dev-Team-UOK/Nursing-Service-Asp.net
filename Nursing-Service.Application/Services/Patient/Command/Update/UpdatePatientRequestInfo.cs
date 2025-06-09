using System.ComponentModel;

namespace Nursing_Service.Application.Services.Patient.Command.Update
{
    public class UpdatePatientRequestInfo
    {
        public ulong Id { get; set; }
        [DisplayName("نام کامل")]
        public string? FullName { get; set; }
        [DisplayName("آدرس")]
        public string? Address { get; set; }
        [DisplayName("سن")]
        public uint? Age { get; set; } 
        [DisplayName("قد")]
        public uint? Height { get; set; }
        [DisplayName("وزن")]
        public uint? Weight { get; set; }
        [DisplayName("شماره همراه")]
        public string? PhoneNumber { get; set; }
        [DisplayName("سوابق بیماری")]
        public string? IllnessHistory { get; set; }
    }
}
