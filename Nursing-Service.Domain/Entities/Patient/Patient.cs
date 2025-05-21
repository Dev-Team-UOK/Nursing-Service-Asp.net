using Nursing_Service.Domain.Entities.Base;
using Nursing_Service.Domain.Entities.SuperVisor;

namespace Nursing_Service.Domain.Entities.Patient
{
    public class Patient : BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public uint Age { get; set; }
        public uint? Height { get; set; }
        public uint? Weight { get; set; }
        public string Address { get; set; }
        public GenderEnum Gender { get; set; }
        public string? IllnessHistory { get; set; }
        public virtual List<PatientNeedService> NeedServices { get; set; }
    }
}
