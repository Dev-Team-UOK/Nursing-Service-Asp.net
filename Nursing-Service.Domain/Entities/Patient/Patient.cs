using Nursing_Service.Domain.Entities.Base;

namespace Nursing_Service.Domain.Entities.Patient
{
    public class Patient : BaseEntity
    {
        public required string NationalCode { get; set; }
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
        public uint Age { get; set; }
        public uint? Height { get; set; }
        public uint? Weight { get; set; }
        public required string Address { get; set; }
        public GenderEnum Gender { get; set; }
        public string? IllnessHistory { get; set; }
        public virtual List<PatientNeedService>? NeedServices { get; set; }
    }
}
