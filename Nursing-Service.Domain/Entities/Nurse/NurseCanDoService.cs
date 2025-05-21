using Nursing_Service.Domain.Entities.Base;

namespace Nursing_Service.Domain.Entities.Nurse
{
    public class NurseCanDoService : BaseEntity
    {
        public virtual Nurse Nurse { get; set; }
        public ulong NurseId { get; set; }
        public virtual Service.Service Service { get; set; }
        public ulong ServiceId { get; set; }
    }
}
